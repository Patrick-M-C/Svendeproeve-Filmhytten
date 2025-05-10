using Microsoft.AspNetCore.Mvc;
using Moq;
using Svendeprøve.Api.Controllers;
using Svendeprøve.Repo.DTO;
using Svendeprøve.Repo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Svendeprøve.Tests.ControllerTests
{
    public class SeatControllerTests
    {
        private readonly Mock<ISeat> _mockRepo;
        private readonly SeatController _controller;

        public SeatControllerTests()
        {
            _mockRepo = new Mock<ISeat>();
            _controller = new SeatController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetSeats_ShouldReturnAllSeats()
        {
            var seats = new List<Seat>
            {
                new Seat { Id = 1, Row = 1, SeatNumber = 1, HallId = 1 },
                new Seat { Id = 2, Row = 1, SeatNumber = 2, HallId = 1 }
            };

            _mockRepo.Setup(r => r.getAll()).ReturnsAsync(seats);

            var result = await _controller.GetSeats();

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var returned = Assert.IsAssignableFrom<IEnumerable<Seat>>(ok.Value);
            Assert.Equal(2, returned.Count());
        }

        [Fact]
        public async Task GetSeat_ShouldReturnSeat_WhenExists()
        {
            var seat = new Seat { Id = 1, Row = 1, SeatNumber = 1, HallId = 1 };
            _mockRepo.Setup(r => r.getById(1)).ReturnsAsync(seat);

            var result = await _controller.GetSeat(1);

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var returned = Assert.IsType<Seat>(ok.Value);
            Assert.Equal(1, returned.Id);
        }

        [Fact]
        public async Task GetSeat_ShouldReturn404_WhenNotFound()
        {
            _mockRepo.Setup(r => r.getById(99)).ReturnsAsync((Seat)null);

            var result = await _controller.GetSeat(99);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetSeatsByHall_ShouldReturnSeatsInHall()
        {
            var seats = new List<Seat>
            {
                new Seat { Id = 1, Row = 1, SeatNumber = 1, HallId = 2 },
                new Seat { Id = 2, Row = 1, SeatNumber = 2, HallId = 2 }
            };

            _mockRepo.Setup(r => r.getByHallId(2)).ReturnsAsync(seats);

            var result = await _controller.GetSeatsByHall(2);

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var returned = Assert.IsAssignableFrom<IEnumerable<Seat>>(ok.Value);
            Assert.All(returned, s => Assert.Equal(2, s.HallId));
        }

        [Fact]
        public async Task GetReservedSeats_ShouldReturnOnlyReservedSeats()
        {
            // Arrange
            var allSeats = new List<Seat>
            {
                new Seat { Id = 1, IsReserved = true },
                new Seat { Id = 2, IsReserved = false },
                new Seat { Id = 3, IsReserved = true }
            };

            _mockRepo.Setup(r => r.getAll()).ReturnsAsync(allSeats);

            // Act
            var result = await _controller.GetReservedSeats();

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var reservedSeats = Assert.IsAssignableFrom<List<Seat>>(ok.Value);

            Assert.Equal(2, reservedSeats.Count);
            Assert.All(reservedSeats, s => Assert.True(s.IsReserved));
        }

        [Fact]
        public async Task CreateSeat_ShouldReturnCreatedSeat()
        {
            var seat = new Seat { Row = 1, SeatNumber = 3, HallId = 1 };
            var created = new Seat { Id = 1, Row = 1, SeatNumber = 3, HallId = 1 };

            _mockRepo.Setup(r => r.create(seat.Row, seat.SeatNumber, seat.HallId)).ReturnsAsync(created);

            var result = await _controller.CreateSeat(seat);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returned = Assert.IsType<Seat>(createdResult.Value);
            Assert.Equal(1, returned.Id);
        }

        [Fact]
        public async Task CreateMultipleSeats_ShouldReturnCreatedSeats()
        {
            var createdSeats = new List<Seat>
            {
                new Seat { Id = 1, Row = 1, SeatNumber = 1, HallId = 1 },
                new Seat { Id = 2, Row = 1, SeatNumber = 2, HallId = 1 }
            };

            _mockRepo.Setup(r => r.CreateMultipleAsync(1, 2, 1)).ReturnsAsync(createdSeats);

            var result = await _controller.CreateMultipleSeats(1, 2, 1);

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var seats = Assert.IsAssignableFrom<List<Seat>>(ok.Value);
            Assert.Equal(2, seats.Count);
        }

        [Fact]
        public async Task UpdateSeat_ShouldReturn204_WhenSuccessful()
        {
            var seat = new Seat { Id = 1, Row = 2, SeatNumber = 5, HallId = 1 };
            _mockRepo.Setup(r => r.update(seat)).ReturnsAsync(seat);

            var result = await _controller.UpdateSeat(1, seat);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateSeat_ShouldReturn400_WhenIdMismatch()
        {
            var seat = new Seat { Id = 2, Row = 2, SeatNumber = 5, HallId = 1 };

            var result = await _controller.UpdateSeat(1, seat);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateSeat_ShouldReturn404_WhenNotFound()
        {
            var seat = new Seat { Id = 1, Row = 2, SeatNumber = 5, HallId = 1 };
            _mockRepo.Setup(r => r.update(seat)).ReturnsAsync((Seat)null);

            var result = await _controller.UpdateSeat(1, seat);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteSeat_ShouldReturn204_WhenDeleted()
        {
            var seat = new Seat { Id = 1 };
            _mockRepo.Setup(r => r.delete(1)).ReturnsAsync(seat);

            var result = await _controller.DeleteSeat(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteSeat_ShouldReturn404_WhenNotFound()
        {
            _mockRepo.Setup(r => r.delete(99)).ReturnsAsync((Seat)null);

            var result = await _controller.DeleteSeat(99);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
