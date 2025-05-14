using Svendeprøve.Repo.DTO;


namespace Svendeprøve.Repo.Interface
{
    public interface IMovie
    {
        Task<IEnumerable<Movie>> GetAllMoviesAsync();    
        Task<Movie> GetMovieByIdAsync(int id);        
    }
}
