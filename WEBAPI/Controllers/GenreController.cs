using Common.DALModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEBAPI.Models;

namespace WEBAPI.Controllers
{
    [Route("api/Genre")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly Common.DALModels.RwaMoviesContext _context;

        public GenreController(Common.DALModels.RwaMoviesContext context)
        {
            _context = (Common.DALModels.RwaMoviesContext)context;
        }


        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<Common.DALModels.Genre>>> GetAll()
        {
            Console.WriteLine(_context);

            try
            {
                return await _context.Genres.ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "There has been a problem while fetching the data you requested");
            }
        }

        [HttpGet("[action]")]
        public ActionResult<IEnumerable<Common.DALModels.Genre>> Search(string searchPart)
        {
            try
            {
                var dbGenres = _context.Genres.Where(x => x.Name.Contains(searchPart));
                return Ok(dbGenres);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "There has been a problem while fetching the data you requested");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Common.DALModels.Genre> Get(int id)
        {
            try
            {
                var dbGenre = _context.Genres.FirstOrDefault(x => x.Id == id);
                if (dbGenre == null)
                    return NotFound($"Could not find genre with id {id}");

                return Ok(dbGenre);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "There has been a problem while fetching the data you requested");
            }
        }

        [HttpPost()]
        public ActionResult<Common.DALModels.Genre> Post(Common.DALModels.Genre genre)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _context.Genres.Add(genre);

                _context.SaveChanges();

                return Ok(genre);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "There has been a problem while fetching the data you requested");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Common.DALModels.Genre> Put(int id, Common.DALModels.Genre genre)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var dbGenre = _context.Genres.FirstOrDefault(x => x.Id == id);
                if (dbGenre == null)
                    return NotFound($"Could not find genre with id {id}");

                dbGenre.Name = genre.Name;
                dbGenre.Description = genre.Description;

                _context.SaveChanges();

                return Ok(dbGenre);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "There has been a problem while fetching the data you requested");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Common.DALModels.Genre> Delete(int id)
        {
            try
            {
                var dbGenre = _context.Genres.FirstOrDefault(x => x.Id == id);
                if (dbGenre == null)
                    return NotFound($"Could not find genre with id {id}");

                _context.Genres.Remove(dbGenre);

                _context.SaveChanges();

                return Ok(dbGenre);
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

