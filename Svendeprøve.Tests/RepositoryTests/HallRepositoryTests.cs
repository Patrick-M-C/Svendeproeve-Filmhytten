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
                .UseInMemoryDatabase(databaseName: "HallRepositoryTests")
                .Options;

            _context = new(_options);
            _hallRepo = new(_context);
        }

        [Fact]
        public async Task GetAllHallsAsync_ShouldReturnListOfHall_WhenHallExists()
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

        [Fact]
        public async Task GetAllHallsAsync_ShouldReturnStringName_WhenHallExists()
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

            foreach (var hall in result)
            {
                Assert.NotNull(hall.Name);
                Assert.IsType<string>(hall.Name);
            }
        }

        [Fact]
        public async Task GetAllHallsAsync_ShouldReturnIntSeatCount_WhenHallExists()
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

            foreach (var hall in result)
            {
                Assert.NotNull(hall.Name);
                Assert.IsType<int>(hall.SeatCount);
            }
        }

        [Fact]
        public async Task GetAllHallsIncludeSeats_ShouldReturnEmptyList_WhenNoHallsExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _hallRepo.getAllIncludeSeats();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnHall_WhenIdExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            var hall = new Hall { Id = 1, Name = "Hall 1", SeatCount = 10 };
            _context.Hall.Add(hall);
            await _context.SaveChangesAsync();

            // Act
            var result = await _hallRepo.getById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(hall.Id, result.Id);
            Assert.Equal("Hall 1", result.Name);
            Assert.Equal(10, result.SeatCount);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenIdDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            await _context.SaveChangesAsync();

            // Act
            var result = await _hallRepo.getById(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectType()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            _context.Hall.Add(new Hall { Id = 2, Name = "Hall 2", SeatCount = 10 });
            await _context.SaveChangesAsync();

            // Act
            var result = await _hallRepo.getById(2);

            // Assert
            Assert.IsType<Hall>(result);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddNewHallToDatabase()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            var hall = new Hall { Name = "New Hall", SeatCount = 40 };

            // Act
            var result = await _hallRepo.create(hall);

            // Assert
            var fromDb = await _context.Hall.FirstOrDefaultAsync(h => h.Id == result.Id);
            Assert.NotNull(fromDb);
            Assert.Equal("New Hall", fromDb.Name);
            Assert.Equal(40, fromDb.SeatCount);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnHallWithGeneratedId()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            var hall = new Hall { Name = "Hall 1", SeatCount = 25 };

            // Act
            var result = await _hallRepo.create(hall);

            // Assert
            Assert.True(result.Id > 0);
        }

        [Fact]
        public async Task CreateAsync_ShouldThrowException_WhenHallIsNull()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _hallRepo.create(null));
        }

        [Fact]
        public async Task Update_ShouldModifySeatCount_WhenHallExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            var hall = new Hall { Name = "Hall 1", SeatCount = 10 };
            _context.Hall.Add(hall);
            await _context.SaveChangesAsync();

            // Act
            var updatedHall = new Hall { Id = hall.Id, SeatCount = 20 };
            var result = await _hallRepo.update(updatedHall);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(20, result.SeatCount);
            Assert.Equal(hall.Id, result.Id);
        }

        [Fact]
        public async Task Update_ShouldReturnNull_WhenHallDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            var fakeHall = new Hall { Id = 99, SeatCount = 10 };

            // Act
            var result = await _hallRepo.update(fakeHall);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Update_ShouldNotChangeName_WhenNotSet()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            var hall = new Hall { Name = "SameName", SeatCount = 40 };
            _context.Hall.Add(hall);
            await _context.SaveChangesAsync();

            // Act
            var updateOnlySeatCount = new Hall { Id = hall.Id, Name = "SameName", SeatCount = 100 };
            var result = await _hallRepo.update(updateOnlySeatCount);

            // Assert
            Assert.Equal("SameName", result.Name);
            Assert.Equal(100, result.SeatCount);
        }

        [Fact]
        public async Task Delete_ShouldRemoveHall_WhenExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            var hall = new Hall { Name = "hall 1", SeatCount = 20 };
            _context.Hall.Add(hall);
            await _context.SaveChangesAsync();

            // Act
            var result = await _hallRepo.delete(hall.Id);

            // Assert
            var fromDb = await _context.Hall.FindAsync(hall.Id);
            Assert.NotNull(result); // Should return the deleted Hall
            Assert.Equal(hall.Id, result.Id);
            Assert.Null(fromDb); // Should no longer exist in DB
        }

        [Fact]
        public async Task Delete_ShouldReturnNull_WhenHallDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _hallRepo.delete(99);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Delete_ShouldNotThrow_WhenDeletingNonexistentId()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act & Assert
            var exception = await Record.ExceptionAsync(() => _hallRepo.delete(99));
            Assert.Null(exception);
        }

    }
}
