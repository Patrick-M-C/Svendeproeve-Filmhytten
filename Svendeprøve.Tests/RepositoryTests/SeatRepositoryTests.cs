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
    public class SeatRepositoryTests
    {
        private readonly DbContextOptions<Databasecontext> _options;
        private readonly Databasecontext _context;
        private readonly SeatRepository _seatRepo;

        public SeatRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<Databasecontext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new(_options);
            _seatRepo = new(_context);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllSeats()
        {
            await _context.Database.EnsureDeletedAsync();
            _context.Seat.AddRange(
                new Seat { Row = 1, SeatNumber = 1, IsReserved = false, HallId = 1 },
                new Seat { Row = 1, SeatNumber = 2, IsReserved = true, HallId = 1 }
            );
            await _context.SaveChangesAsync();

            var result = await _seatRepo.getAll();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetById_ShouldReturnSeat_WhenExists()
        {
            await _context.Database.EnsureDeletedAsync();
            var seat = new Seat { Row = 2, SeatNumber = 5, IsReserved = false, HallId = 1 };
            _context.Seat.Add(seat);
            await _context.SaveChangesAsync();

            var result = await _seatRepo.getById(seat.Id);

            Assert.NotNull(result);
            Assert.Equal(seat.SeatNumber, result.SeatNumber);
        }

        [Fact]
        public async Task GetById_ShouldReturnNull_WhenNotFound()
        {
            await _context.Database.EnsureDeletedAsync();

            var result = await _seatRepo.getById(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetByHallId_ShouldReturnSeatsForHall()
        {
            await _context.Database.EnsureDeletedAsync();
            _context.Seat.AddRange(
                new Seat { Row = 1, SeatNumber = 1, IsReserved = false, HallId = 1 },
                new Seat { Row = 2, SeatNumber = 2, IsReserved = false, HallId = 2 }
            );
            await _context.SaveChangesAsync();

            var result = await _seatRepo.getByHallId(1);

            Assert.Single(result);
            Assert.All(result, s => Assert.Equal(1, s.HallId));
        }

        [Fact]
        public async Task IsSeatReservedAsync_ShouldReturnTrue_WhenReserved()
        {
            await _context.Database.EnsureDeletedAsync();
            var seat = new Seat { Row = 3, SeatNumber = 10, IsReserved = true, HallId = 1 };
            _context.Seat.Add(seat);
            await _context.SaveChangesAsync();

            var result = await _seatRepo.IsSeatReservedAsync(seat.Id);

            Assert.True(result);
        }

        [Fact]
        public async Task IsSeatReservedAsync_ShouldThrow_WhenNotFound()
        {
            await _context.Database.EnsureDeletedAsync();

            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                _seatRepo.IsSeatReservedAsync(999));
        }

        [Fact]
        public async Task Create_ShouldAddSeat()
        {
            await _context.Database.EnsureDeletedAsync();

            var result = await _seatRepo.create(1, 5, 1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Row);
            Assert.Equal(5, result.SeatNumber);
        }

        [Fact]
        public async Task CreateMultipleAsync_ShouldAddCorrectNumberOfSeats()
        {
            await _context.Database.EnsureDeletedAsync();

            var result = await _seatRepo.CreateMultipleAsync(2, 3, 1); // 2 rows x 3 seats

            Assert.Equal(6, result.Count);
            Assert.All(result, s => Assert.Equal(1, s.HallId));
        }

        [Fact]
        public async Task Delete_ShouldRemoveSeat_WhenExists()
        {
            await _context.Database.EnsureDeletedAsync();
            var seat = new Seat { Row = 1, SeatNumber = 1, HallId = 1 };
            _context.Seat.Add(seat);
            await _context.SaveChangesAsync();

            var result = await _seatRepo.delete(seat.Id);

            Assert.NotNull(result);
            var deleted = await _context.Seat.FindAsync(seat.Id);
            Assert.Null(deleted);
        }

        [Fact]
        public async Task Delete_ShouldReturnNull_WhenNotFound()
        {
            await _context.Database.EnsureDeletedAsync();

            var result = await _seatRepo.delete(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task Update_ShouldModifySeatProperties()
        {
            await _context.Database.EnsureDeletedAsync();
            var seat = new Seat { Row = 1, SeatNumber = 3, HallId = 1 };
            _context.Seat.Add(seat);
            await _context.SaveChangesAsync();

            var updated = new Seat { Id = seat.Id, SeatNumber = 10, HallId = 2 };
            var result = await _seatRepo.update(updated);

            Assert.Equal(10, result.SeatNumber);
            Assert.Equal(2, result.HallId);
        }
    }
}
