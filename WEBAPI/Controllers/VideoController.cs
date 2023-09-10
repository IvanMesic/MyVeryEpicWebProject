using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WEBAPI.BLModels;
using WEBAPI.Mapping;
using WEBAPI.Models;
using System.Drawing;
using Common.DALModels;

namespace WEBAPI.Controllers
{
    [Route("api/Video")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private RwaMoviesContext _context;

        public VideoController(DbContext context)
        {
            _context = (RwaMoviesContext)context;
        }

        #region CRUD


        // GET: ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Video>> GetVideo(int id)
        {
            var video = await _context.Videos.FindAsync(id);

            if (video == null)
            {
                return NotFound();
            }

            return video;
        }
        //PUT:
        [HttpPut("put/{id}")]
        public async Task<IActionResult> UpdateVideo(int id, [FromBody] Video video)
        {
            if (video == null)
            {
                return BadRequest();
            }

            var existingVideo = await _context.Videos.FindAsync(id);

            if (existingVideo == null)
            {
                return NotFound();
            }

            existingVideo.Name = video.Name;
            existingVideo.Description = video.Description;
            existingVideo.Image = video.Image;
            existingVideo.TotalSeconds = video.TotalSeconds;
            existingVideo.StreamingUrl = video.StreamingUrl;
            existingVideo.Genre = video.Genre;
            existingVideo.VideoTags = video.VideoTags;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception);
            }
            return NoContent();
        }


        // POST: 
        [HttpPost()]
        public async Task<ActionResult<BLVideo>> PostVideo(BLVideo video)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var dbVideo = VideoMapping.MapToDAL(video);

                IQueryable<Tag> dbTags = _context.Tags.Where(x => video.VideoTags.Select(y => y.Tag).Any());
                dbVideo.VideoTags = dbTags.Select(x => new VideoTag { Tag = x }).ToList();

                _context.Videos.Add(dbVideo);
                await _context.SaveChangesAsync();
                return video;
            }
            catch (Exception ex)
            {
                return StatusCode(
                       StatusCodes.Status500InternalServerError,
                      "There has been a problem while fetching the data you requested");
            }
        }

        // DELETE:
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideo(int id)
        {
            var genre = await _context.Genres.Include(g => g.Videos)
                .SingleOrDefaultAsync(v => v.Id == id);
            var video = await _context.Videos.Include(v => v.VideoTags)
                .Include(v => v.Genre)
                .SingleOrDefaultAsync(v => v.Id == id);
            if (video == null)
            {
                return NotFound();
            }

            _context.VideoTags.RemoveRange(video.VideoTags);
            _context.Videos.Remove(video);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VideoExists(int id)
        {
            return _context.Videos.Any(e => e.Id == id);
        }

        #endregion


        // GET: filter
        [HttpGet("[action]")]
        public async Task<ActionResult<List<Video>>> Search(int? min, int? max, string orderBy, string direction, int page, int size)
        {
            try
            {
                // Cookie handling
                var searchMinStr = HttpContext.Request.Cookies["search.min"];
                if (min.HasValue)
                {
                    // Add/update cookie value
                    HttpContext.Response.Cookies.Append("search.min", min.Value.ToString());

                }
                else if (!string.IsNullOrEmpty(searchMinStr))
                {
                    // Read value if exists
                    min = int.Parse(searchMinStr);
                }

                var searchMaxStr = HttpContext.Request.Cookies["search.max"];
                if (max.HasValue)
                {
                    // Add/update cookie value
                    HttpContext.Response.Cookies.Append("search.max", max.Value.ToString());

                }
                else if (!string.IsNullOrEmpty(searchMaxStr))
                {
                    // Read value if exists
                    min = int.Parse(searchMaxStr);
                }

                IEnumerable<Video> receipts = await _context.Videos.ToListAsync();

                // Filtering
                if (min.HasValue)
                    receipts = receipts.Where(x => x.TotalSeconds >= min);

                if (max.HasValue)
                    receipts = receipts.Where(x => x.TotalSeconds <= max);

                // Ordering
                if (string.Compare(orderBy, "id", true) == 0)
                {
                    receipts = receipts.OrderBy(x => x.Id);
                }
                else if (string.Compare(orderBy, "total", true) == 0)
                {
                    receipts = receipts.OrderBy(x => x.TotalSeconds);
                }
                else // default: order by Id
                {
                    receipts = receipts.OrderBy(x => x.Id);
                }

                // Ordering direction
                if (string.Compare(direction, "desc", true) == 0)
                {
                    receipts = receipts.Reverse();
                }

                // Paging
                //receipts = receipts.Skip(page * size).Take(size);

                // Session handling
                HttpContext.Session.SetString("receipts.search.count", receipts.Count().ToString());

                return receipts.ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
