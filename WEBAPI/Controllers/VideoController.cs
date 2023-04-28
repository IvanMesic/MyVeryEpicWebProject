using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WEBAPI.BLModels;
using WEBAPI.Mapping;
using WEBAPI.Models;



namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private  RwaMoviesContext _context;

        public VideoController(DbContext context)
        {
            _context = (RwaMoviesContext?)context;
        }

        // GET: api/video
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Video>>> GetVideos()
        {
            return await _context.Videos.ToListAsync();
        }

        // GET: api/video/5
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

        // PUT: api/video/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVideo(int id, Video video)
        {
            if (id != video.Id)
            {
                return BadRequest();
            }

            _context.Entry(video).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VideoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/video
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

        // DELETE: api/video/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideo(int id)
        {
            var video = await _context.Videos.FirstOrDefaultAsync(x=>x.Id==id);
            if (video == null)
            {
                return NotFound();
            }

            _context.Videos.Remove(video);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VideoExists(int id)
        {
            return _context.Videos.Any(e => e.Id == id);
        }
    }
}
