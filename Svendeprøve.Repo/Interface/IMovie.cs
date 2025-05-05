using Svendeprøve.Repo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Svendeprøve.Repo.Interface
{
    public interface IMovie
    {
        Task<IEnumerable<Movie>> GetAllMoviesratelimitAsync();
        Task<IEnumerable<Movie>> GetAllMoviesAsync();
        Task<Movie> GetMovieByIdAsync(int id);
        Task<List<Movie>> GetMoviesAsync(int page, int pageSize);
    }
}
