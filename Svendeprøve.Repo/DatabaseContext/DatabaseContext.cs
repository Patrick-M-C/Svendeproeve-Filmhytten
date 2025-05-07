using Microsoft.EntityFrameworkCore;
using Svendeprøve.Repo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Svendeprøve.Repo.DatabaseContext
{
    public class Databasecontext : DbContext
    {
        public Databasecontext(DbContextOptions<Databasecontext> options) : base(options) { }
        public DbSet<Hall> Hall { get; set; }
        public DbSet<Seat> Seat { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Genre> Genre { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasData(
                new Genre { id = 28, name = "Action" },
                new Genre { id = 12, name = "Adventure" },
                new Genre { id = 16, name = "Animation" },
                new Genre { id = 35, name = "Comedy" },
                new Genre { id = 80, name = "Crime" },
                new Genre { id = 99, name = "Documentary" },
                new Genre { id = 18, name = "Drama" },
                new Genre { id = 10751, name = "Family" },
                new Genre { id = 14, name = "Fantasy" },
                new Genre { id = 36, name = "History" },
                new Genre { id = 27, name = "Horror" },
                new Genre { id = 10402, name = "Music" },
                new Genre { id = 9648, name = "Mystery" },
                new Genre { id = 10749, name = "Romance" },
                new Genre { id = 878, name = "Science Fiction" },
                new Genre { id = 10770, name = "TV Movie" },
                new Genre { id = 53, name = "Thriller" },
                new Genre { id = 10752, name = "War" },
                new Genre { id = 37, name = "Western" }
            );
        }

    }
}
