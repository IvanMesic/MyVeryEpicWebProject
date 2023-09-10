using Common.DALModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using WEBAPI.BLModels;
using WEBAPI.Models;

namespace WEBAPI.Controllers
{
    [Route("api/Tag")]
    public class TagController : Controller
    {

        private RwaMoviesContext _context;

        public TagController(DbContext context)
        {
            _context = (RwaMoviesContext)context;
        }

        // GET: All
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<Tag>>> GetAll()
        {
            try
            {
                return await _context.Tags.ToListAsync();
            }
            catch (Exception ex)
            {

                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "There has been a problem while fetching the data you requested");

            }
        }

        // GET: Details
        [HttpGet("Namefilter")]
        public ActionResult<IEnumerable<Tag>> Search
            ([FromQuery] int page,
             [FromQuery] int PageSize,
             [FromQuery] string filter,
             [FromQuery] string orderBy)

        {
            try
            {
                IQueryable<Tag> query = _context.Tags.AsQueryable();
                if (!string.IsNullOrEmpty(filter))
                {
                    query = query.Where(q => q.Name.Contains(filter));
                }

                switch (orderBy.ToLower())
                {
                    case "name":
                        query = query.OrderBy(q => q.Name);
                        break;

                }

                var tags = _context.Tags.Where(x => x.Name.Contains(filter));
                return Ok(tags);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "There has been a problem while fetching the data you requested");
            }
        }
        //get
        [HttpGet("{ID}")]
        public ActionResult<IEnumerable<Tag>> GetTag(int id)
        {
            try
            {
                var tag = _context.Tags.FindAsync(id);
                return Ok(tag);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "There has been a problem while fetching the data you requested");
            }
        }


        // POST:

        //[ValidateAntiForgeryToken]
        [HttpPost("[action]")]
        public ActionResult<IEnumerable<Tag>> CreateTag(Tag tag)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                _context.Tags.Add(tag);
                _context.SaveChanges();
                return Ok(tag);

            }
            catch
            {
                return View();
            }
        }



        // POST
        [HttpPut("{id}")]
        //[ValidateAntiForgeryToken]
        public ActionResult<Tag> Edit(int id, [FromBody] Tag tag)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var dbTags = _context.Tags.FirstOrDefault(t => t.Id == id);
                if (dbTags == null)
                    return NotFound($"Could not find Tag with id {id}");
                dbTags.Name = tag.Name;
                _context.SaveChanges();
                return Ok(tag);
            }
            catch (Exception ex)
            {
                return StatusCode(
                                    StatusCodes.Status500InternalServerError,
                                    "There has been a problem while fetching the data you requested");
            }
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {

            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }

            var videoTags = _context.VideoTags.Where(vt => vt.TagId == id);

            _context.VideoTags.RemoveRange(videoTags);

            _context.Tags.Remove(tag);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TagExists(int id)
        {
            return _context.Tags.Any(e => e.Id == id);
        }
        // POST
        [HttpPost("{id}")]
        public ActionResult<BLTag> Post(BLTag tag)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _context.SaveChanges();

                return Ok(tag);
            }
            catch (Exception ex)
            {
                return StatusCode(
    StatusCodes.Status500InternalServerError,
    "There has been a problem while fetching the data you requested");

            }
        }
    }
}
