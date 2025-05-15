using Microsoft.AspNetCore.Mvc;
using Svendeprøve.Repo.DTO;
using Svendeprøve.Repo.Interface;

namespace Svendeprøve.Api.Controllers
{
    /*
     * MovieController
     * 
     * Giver adgang til filmrelaterede data via API-kald. 
     * Film data hentes fra TMDB API via et repository, 
     * hvilket sikrer en tydelig adskillelse mellem logik og datahåndtering.
     * 
     * Funktionalitet:
     * - Returnerer en liste af alle tilgængelige film.
     * - Muliggør opslag af en enkelt film ved hjælp af dens ID.
     */


    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovie _movieRepo;

        public MovieController(IMovie movieRepo)
        {
            _movieRepo = movieRepo;
        }

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
