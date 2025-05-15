using System.ComponentModel.DataAnnotations;

namespace Svendeprøve.Repo.DTO
{
    // Genre-klassen repræsenterer en filmgenre fra TMDB API'et.
    // Den bruges til at matche genre-id'er fra filmdata med deres tilsvarende navne.
    public class Genre
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
    }

}


