using System.ComponentModel.DataAnnotations;

namespace Svendeprøve.Repo.DTO
{
    public class Genre
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
    }

}
