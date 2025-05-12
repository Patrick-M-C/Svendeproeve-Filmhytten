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
    public class UserControllerTests
    {
        private readonly Mock<IUser> _mockRepo;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _mockRepo = new Mock<IUser>();
            _controller = new UserController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetUsers_ShouldReturnAllUsers()
        {
            var users = new List<User>
            {
                new User { Id = 1, Name = "Alice" },
                new User { Id = 2, Name = "Kasper" }
            };

            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(users);

            var result = await _controller.GetUsers();

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var returned = Assert.IsAssignableFrom<List<User>>(ok.Value);
            Assert.Equal(2, returned.Count);
        }

        [Fact]
        public async Task GetUsersIncludeTickets_ShouldReturnUsersWithTickets()
        {
            _mockRepo.Setup(r => r.GetAllIncludeUserTicketAsync()).ReturnsAsync(new List<User>());

            var result = await _controller.GetUsersIncludeTickets();

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsAssignableFrom<IEnumerable<User>>(ok.Value);
        }

        [Fact]
        public async Task GetUser_ShouldReturnUser_WhenExists()
        {
            var user = new User { Id = 1, Name = "Test" };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(user);

            var result = await _controller.GetUser(1);

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var returned = Assert.IsType<User>(ok.Value);
            Assert.Equal("Test", returned.Name);
        }

        [Fact]
        public async Task GetUser_ShouldReturn404_WhenNotFound()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((User)null);

            var result = await _controller.GetUser(99);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetByEmail_ShouldReturnUser_WhenExists()
        {
            var user = new User { Email = "test@example.com" };
            _mockRepo.Setup(r => r.GetByEmailAsync("test@example.com")).ReturnsAsync(user);

            var result = await _controller.GetByEmail("test@example.com");

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var returned = Assert.IsType<User>(ok.Value);
            Assert.Equal("test@example.com", returned.Email);
        }

        [Fact]
        public async Task GetByEmail_ShouldReturn404_WhenNotFound()
        {
            _mockRepo.Setup(r => r.GetByEmailAsync("testg@example.com")).ReturnsAsync((User)null);

            var result = await _controller.GetByEmail("test@example.com");

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateUser_ShouldReturnCreatedUser()
        {
            var newUser = new User { Id = 1, Name = "New" };
            _mockRepo.Setup(r => r.CreateAsync(It.IsAny<User>())).ReturnsAsync(newUser);

            var result = await _controller.CreateUser(newUser);

            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returned = Assert.IsType<User>(created.Value);
            Assert.Equal("New", returned.Name);
        }

        [Fact]
        public async Task CreateUser_ShouldReturnBadRequest_WhenNull()
        {
            var result = await _controller.CreateUser(null);

            var bad = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("User cannot be null.", bad.Value);
        }

        [Fact]
        public async Task UpdateUser_ShouldReturn204_WhenSuccessful()
        {
            var user = new User { Id = 1, Name = "Updated" };
            _mockRepo.Setup(r => r.UpdateAsync(user)).ReturnsAsync(user);

            var result = await _controller.UpdateUser(1, user);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateUser_ShouldReturn400_WhenIdMismatch()
        {
            var user = new User { Id = 2, Name = "Mismatch" };

            var result = await _controller.UpdateUser(1, user);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateUser_ShouldReturn404_WhenNotFound()
        {
            var user = new User { Id = 1, Name = "Missing" };
            _mockRepo.Setup(r => r.UpdateAsync(user)).ThrowsAsync(new KeyNotFoundException());

            var result = await _controller.UpdateUser(1, user);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteUser_ShouldReturn204_WhenDeleted()
        {
            var user = new User { Id = 1 };
            _mockRepo.Setup(r => r.DeleteAsync(1)).ReturnsAsync(user);

            var result = await _controller.DeleteUser(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteUser_ShouldReturn404_WhenNotFound()
        {
            _mockRepo.Setup(r => r.DeleteAsync(99)).ReturnsAsync((User)null);

            var result = await _controller.DeleteUser(99);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
