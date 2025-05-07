using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Svendeprøve.Repo.DTO;
using Svendeprøve.Repo.Interface;

namespace Svendeprøve.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovie _movieRepo;

        public MovieController(IMovie movieRepo)
        {
            _movieRepo = movieRepo;
        }

        // GET api/movie?page=1&pageSize=40
        //[HttpGet]
        //public async Task<IActionResult> GetMovies(int page = 1, int pageSize = 40)
        //{
        //    try
        //    {
        //        var movies = await _movieRepo.GetMoviesAsync(page, pageSize);
        //        if (movies == null || movies.Count() == 0)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(movies);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Optionally log the exception here.
        //        return StatusCode(500, $"An error occurred: {ex.Message}");
        //    }
        //}

        
        //[HttpGet("ratelimit")]
        //public async Task<ActionResult<IEnumerable<Movie>>> GetAllratelimitMovies()
        //{
        //    var movies = await _movieRepo.GetAllMoviesratelimitAsync();
        //    if (movies == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(movies);
        //}

        //// GET: api/Movie
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetAllMovies()
        {
            var movies = await _movieRepo.GetAllMoviesAsync();
            return Ok(movies);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovieById(int id)
        {
            var movie = await _movieRepo.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }
    }
}
