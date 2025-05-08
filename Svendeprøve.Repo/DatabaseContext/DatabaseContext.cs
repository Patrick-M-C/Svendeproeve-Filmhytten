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

            modelBuilder.Entity<User>().HasData(
               new User { Id = 1, Name = "Kasper", Email = "kasper@mail.com", PhoneNumber = "123456789", Password = "placeholder1" },
               new User { Id = 2, Name = "Sofia", Email = "sofia@mail.com", PhoneNumber = "987654321", Password = "placeholder2" }
            );

            modelBuilder.Entity<Ticket>().HasData(
               new Ticket { Id = 1, UserId = 1, SeatId = 1, /*ScreeningId = 1,*/ Price = 75, IsCanceled = false },
               new Ticket { Id = 2, UserId = 1, SeatId = 2, /*ScreeningId = 1,*/ Price = 75, IsCanceled = false },
               new Ticket { Id = 3, UserId = 2, SeatId = 4, /*ScreeningId = 2,*/ Price = 95, IsCanceled = false },
               new Ticket { Id = 4, UserId = 2, SeatId = 4, /*ScreeningId = 1,*/ Price = 75, IsCanceled = true }
            );

            modelBuilder.Entity<Hall>().HasData(
               new Hall { Id = 1, SeatCount = 9, Name = "Hall 1" },
               new Hall { Id = 2, SeatCount = 4, Name = "Hall 2" },
               new Hall { Id = 3, SeatCount = 4, Name = "Hall 3" });

            modelBuilder.Entity<Seat>().HasData(
                    // Seats for Hall 1
                    new Seat { Id = 1, Row = 1, SeatNumber = 1, IsReserved = false, HallId = 1 },
                    new Seat { Id = 2, Row = 1, SeatNumber = 2, IsReserved = false, HallId = 1 },
                    new Seat { Id = 3, Row = 1, SeatNumber = 3, IsReserved = false, HallId = 1 },
                    new Seat { Id = 4, Row = 2, SeatNumber = 4, IsReserved = false, HallId = 1 },
                    new Seat { Id = 5, Row = 2, SeatNumber = 5, IsReserved = false, HallId = 1 },
                    new Seat { Id = 6, Row = 2, SeatNumber = 6, IsReserved = false, HallId = 1 },
                    new Seat { Id = 7, Row = 3, SeatNumber = 7, IsReserved = true, HallId = 1 },
                    new Seat { Id = 8, Row = 3, SeatNumber = 8, IsReserved = true, HallId = 1 },
                    new Seat { Id = 9, Row = 3, SeatNumber = 9, IsReserved = false, HallId = 1 },

                    // Seats for Hall 2
                    new Seat { Id = 10, Row = 1, SeatNumber = 1, IsReserved = false, HallId = 2 },
                    new Seat { Id = 11, Row = 1, SeatNumber = 2, IsReserved = true, HallId = 2 },
                    new Seat { Id = 12, Row = 2, SeatNumber = 3, IsReserved = false, HallId = 2 },
                    new Seat { Id = 13, Row = 2, SeatNumber = 4, IsReserved = false, HallId = 2 },

                    // Seats for Hall 3
                    new Seat { Id = 14, Row = 1, SeatNumber = 1, IsReserved = true, HallId = 3 },
                    new Seat { Id = 15, Row = 1, SeatNumber = 2, IsReserved = false, HallId = 3 },
                    new Seat { Id = 16, Row = 2, SeatNumber = 3, IsReserved = false, HallId = 3 },
                    new Seat { Id = 17, Row = 2, SeatNumber = 4, IsReserved = false, HallId = 3 }
                );
        }

    }
}
