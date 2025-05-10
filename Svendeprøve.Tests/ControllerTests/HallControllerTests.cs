using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Svendeprøve.Api.Controllers;
using Svendeprøve.Repo.DatabaseContext;
using Svendeprøve.Repo.DTO;
using Svendeprøve.Repo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Svendeprøve.Tests.ControllerTests
{
    public class HallControllerTests
    {
        private readonly Mock<IHall> _mockRepo;
        private readonly HallController _controller;

        public HallControllerTests()
        {
            _mockRepo = new Mock<IHall>();
            _controller = new HallController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetHalls_ShouldReturnAllHalls()
        {
            // Arrange
            var halls = new List<Hall>
            {
            new Hall { Id = 1, Name = "Hall A", SeatCount = 100 },
            new Hall { Id = 2, Name = "Hall B", SeatCount = 150 }
            };

            _mockRepo.Setup(r => r.getAll()).ReturnsAsync(halls);

            // Act
            var result = await _controller.GetHalls();

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var returnedHalls = Assert.IsAssignableFrom<IEnumerable<Hall>>(ok.Value);
            Assert.Equal(2, ((List<Hall>)returnedHalls).Count);
        }

        [Fact]
        public async Task GetHallsWithSeats_ShouldReturnHallsWithSeats()
        {
            _mockRepo.Setup(r => r.getAllIncludeSeats()).ReturnsAsync(new List<Hall>());

            var result = await _controller.GetHallsWithSeats();

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsAssignableFrom<IEnumerable<Hall>>(ok.Value);
        }

        [Fact]
        public async Task GetHallById_ShouldReturnHall_WhenExists()
        {
            var hall = new Hall { Id = 1, Name = "Main", SeatCount = 200 };
            _mockRepo.Setup(r => r.getById(1)).ReturnsAsync(hall);

            var result = await _controller.GetHallbyid(1);

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var returned = Assert.IsType<Hall>(ok.Value);
            Assert.Equal("Main", returned.Name);
        }

        [Fact]
        public async Task GetHallById_ShouldReturn404_WhenNotFound()
        {
            _mockRepo.Setup(r => r.getById(99)).ReturnsAsync((Hall)null);

            var result = await _controller.GetHallbyid(99);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetHallWithSeatsById_ShouldReturnHallWithSeats()
        {
            var hall = new Hall { Id = 1, Name = "Seated", SeatCount = 20 };
            _mockRepo.Setup(r => r.getByIdIncludeSeats(1)).ReturnsAsync(hall);

            var result = await _controller.GetHallWithSeatsbyid(1);

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var returned = Assert.IsType<Hall>(ok.Value);
            Assert.Equal("Seated", returned.Name);
        }

        [Fact]
        public async Task GetHallWithSeatsById_ShouldReturn404_WhenNotFound()
        {
            _mockRepo.Setup(r => r.getByIdIncludeSeats(99)).ReturnsAsync((Hall)null);

            var result = await _controller.GetHallWithSeatsbyid(99);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateHall_ShouldReturnCreatedHall()
        {
            var newHall = new Hall { Id = 1, Name = "New", SeatCount = 10 };
            _mockRepo.Setup(r => r.create(It.IsAny<Hall>())).ReturnsAsync(newHall);

            var result = await _controller.CreateHall(newHall);

            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            var hall = Assert.IsType<Hall>(created.Value);
            Assert.Equal("New", hall.Name);
        }

        [Fact]
        public async Task UpdateHall_ShouldReturn204_WhenSuccessful()
        {
            var updatedHall = new Hall { Id = 1, Name = "Updated", SeatCount = 15 };
            _mockRepo.Setup(r => r.update(It.IsAny<Hall>())).ReturnsAsync(updatedHall);

            var result = await _controller.UpdateHall(1, updatedHall);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateHall_ShouldReturn400_WhenIdMismatch()
        {
            var updatedHall = new Hall { Id = 2, Name = "Mismatch", SeatCount = 100 };

            var result = await _controller.UpdateHall(1, updatedHall);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateHall_ShouldReturn404_WhenHallNotFound()
        {
            var hall = new Hall { Id = 1, Name = "Missing", SeatCount = 200 };
            _mockRepo.Setup(r => r.update(hall)).ReturnsAsync((Hall)null);

            var result = await _controller.UpdateHall(1, hall);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteHall_ShouldReturn204_WhenSuccessful()
        {
            var hall = new Hall { Id = 1, Name = "ToDelete", SeatCount = 100 };
            _mockRepo.Setup(r => r.delete(1)).ReturnsAsync(hall);

            var result = await _controller.DeleteHall(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteHall_ShouldReturn404_WhenNotFound()
        {
            _mockRepo.Setup(r => r.delete(99)).ReturnsAsync((Hall)null);

            var result = await _controller.DeleteHall(99);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
