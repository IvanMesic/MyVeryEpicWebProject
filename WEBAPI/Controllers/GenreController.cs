using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEBAPI.Models;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly RwaMoviesContext _context;

        public GenreController(RwaMoviesContext context)
        {
            _context = context;
        }

        private readonly RwaMoviesContext _dbContext;


        [HttpGet("[action]")]
        public ActionResult<IEnumerable<Genre>> GetAll()
        {
            Console.WriteLine(_dbContext);
            
            try
            {
                return _dbContext.Genres;
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "There has been a problem while fetching the data you requested");
            }
        }

        [HttpGet("[action]")]
        public ActionResult<IEnumerable<Genre>> Search(string searchPart)
        {
            try
            {
                var dbGenres = _dbContext.Genres.Where(x => x.Name.Contains(searchPart));
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
        public ActionResult<Genre> Get(int id)
        {
            try
            {
                var dbGenre = _dbContext.Genres.FirstOrDefault(x => x.Id == id);
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
        public ActionResult<Genre> Post(Genre genre)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _dbContext.Genres.Add(genre);

                _dbContext.SaveChanges();

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
        public ActionResult<Genre> Put(int id, Genre genre)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var dbGenre = _dbContext.Genres.FirstOrDefault(x => x.Id == id);
                if (dbGenre == null)
                    return NotFound($"Could not find genre with id {id}");

                dbGenre.Name = genre.Name;
                dbGenre.Description = genre.Description;

                _dbContext.SaveChanges();

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
        public ActionResult<Genre> Delete(int id)
        {
            try
            {
                var dbGenre = _dbContext.Genres.FirstOrDefault(x => x.Id == id);
                if (dbGenre == null)
                    return NotFound($"Could not find genre with id {id}");

                _dbContext.Genres.Remove(dbGenre);

                _dbContext.SaveChanges();

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

  