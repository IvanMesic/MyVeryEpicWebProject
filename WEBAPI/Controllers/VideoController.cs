using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Common.DALModels;
using WEBAPI.BLModels;
using WEBAPI.Mapping;
using WEBAPI.Models;
using System.Collections.Generic;
using AutoMapper;

namespace WEBAPI.Controllers
{
    [Route("api/Video")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly RwaMoviesContext _context;
        private readonly IMapper _mapper;

        public VideoController(RwaMoviesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region CRUD

        // GET: api/Video/5
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

        // PUT: api/Video/put/5
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

        // POST: api/Video
        [HttpPost]
        public async Task<ActionResult<BLVideo>> PostVideo(BLVideo video)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var dbVideo = _mapper.Map<Video>(video); // AutoMapper mapping

                IQueryable<Tag> dbTags = _context.Tags.Where(x => video.VideoTags.Select(y => y.Tag).Any());
                dbVideo.VideoTags = dbTags.Select(x => new VideoTag { Tag = x }).ToList();

                _context.Videos.Add(dbVideo);
                await _context.SaveChangesAsync();

                var blVideo = _mapper.Map<BLVideo>(dbVideo); // AutoMapper mapping

                return blVideo;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        // DELETE: api/Video/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideo(int id)
        {
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

        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<Video>>> Search(int? min, int? max, string orderBy = "id", string direction = "asc", int page = 1, int size = 10, string searchName = null)
        {
            try
            {
                IQueryable<Video> query = _context.Videos;

                // Filtering
                if (min.HasValue)
                {
                    query = query.Where(x => x.TotalSeconds >= min);
                }

                if (max.HasValue)
                {
                    query = query.Where(x => x.TotalSeconds <= max);
                }

                // Search by name
                if (!string.IsNullOrEmpty(searchName))
                {
                    query = query.Where(x => x.Name.Contains(searchName));
                }

                // Ordering
                if (string.Equals(orderBy, "total", StringComparison.OrdinalIgnoreCase))
                {
                    query = direction.ToLower() == "desc"
                        ? query.OrderByDescending(x => x.TotalSeconds)
                        : query.OrderBy(x => x.TotalSeconds);
                }
                else
                {
                    query = direction.ToLower() == "desc"
                        ? query.OrderByDescending(x => x.Id)
                        : query.OrderBy(x => x.Id);
                }

                // Paging
                var totalCount = await query.CountAsync();
                var totalPages = (int)Math.Ceiling(totalCount / (double)size);

                if (page < 1)
                {
                    page = 1;
                }
                else if (page > totalPages)
                {
                    page = totalPages;
                }

                var pagedResults = await query.Skip((page - 1) * size).Take(size).ToListAsync();

                var result = new
                {
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    CurrentPage = page,
                    PageSize = size,
                    Results = pagedResults
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
