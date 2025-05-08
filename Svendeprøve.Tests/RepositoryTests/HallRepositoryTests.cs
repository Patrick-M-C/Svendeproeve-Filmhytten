using Microsoft.EntityFrameworkCore;
using Svendeprøve.Repo.DatabaseContext;
using Svendeprøve.Repo.DTO;
using Svendeprøve.Repo.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Svendeprøve.Tests.RepositoryTests
{
    public class HallRepositoryTests
    {
        private readonly DbContextOptions<Databasecontext> _options;
        private readonly Databasecontext _context;
        private readonly HallRepository _hallRepo;

        public HallRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<Databasecontext>()
                .UseInMemoryDatabase(databaseName: "HeroRepositoryTests")
                .Options;

            _context = new(_options);
            _hallRepo = new(_context);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnListOfHall_WhenHallExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            _context.Hall.Add(new Hall { Id = 1, Name = "Test", SeatCount = 5 });
            _context.Hall.Add(new Hall { Id = 2, Name = "Test2", SeatCount = 4 });

            await _context.SaveChangesAsync();

            // Act
            var result = await _hallRepo.getAllIncludeSeats();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Hall>>(result);
            Assert.Equal(2, result.Count);
        }






    }
}
