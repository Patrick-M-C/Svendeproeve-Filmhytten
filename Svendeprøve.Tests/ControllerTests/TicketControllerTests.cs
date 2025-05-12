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
    public class TicketControllerTests
    {
        private readonly Mock<ITicket> _mockRepo;
        private readonly TicketController _controller;

        public TicketControllerTests()
        {
            _mockRepo = new Mock<ITicket>();
            _controller = new TicketController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetTickets_ShouldReturnAllTickets()
        {
            var tickets = new List<Ticket>
            {
                new Ticket { Id = 1, Price = 100 },
                new Ticket { Id = 2, Price = 150 }
            };
            _mockRepo.Setup(r => r.getAll()).ReturnsAsync(tickets);

            var result = await _controller.GetTickets();

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var returned = Assert.IsAssignableFrom<List<Ticket>>(ok.Value);
            Assert.Equal(2, returned.Count);
        }

        [Fact]
        public async Task GetTicket_ShouldReturnTicket_WhenExists()
        {
            var ticket = new Ticket { Id = 1, Price = 100 };
            _mockRepo.Setup(r => r.getById(1)).ReturnsAsync(ticket);

            var result = await _controller.GetTicket(1);

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var returned = Assert.IsType<Ticket>(ok.Value);
            Assert.Equal(100, returned.Price);
        }

        [Fact]
        public async Task GetTicket_ShouldReturn404_WhenNotFound()
        {
            _mockRepo.Setup(r => r.getById(99)).ReturnsAsync((Ticket)null);

            var result = await _controller.GetTicket(99);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateTicket_ShouldReturnCreatedTicket()
        {
            var ticket = new Ticket { Id = 1, Price = 120 };
            _mockRepo.Setup(r => r.create(It.IsAny<Ticket>())).ReturnsAsync(ticket);

            var result = await _controller.CreateTicket(ticket);

            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returned = Assert.IsType<Ticket>(created.Value);
            Assert.Equal(120, returned.Price);
        }

        [Fact]
        public async Task CreateTicket_ShouldReturnBadRequest_WhenNull()
        {
            var result = await _controller.CreateTicket(null);

            var bad = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Ticket cannot be null.", bad.Value);
        }

        [Fact]
        public async Task UpdateTicket_ShouldReturn204_WhenSuccessful()
        {
            var ticket = new Ticket { Id = 1, Price = 100 };
            _mockRepo.Setup(r => r.update(It.IsAny<Ticket>())).ReturnsAsync(ticket);

            var result = await _controller.UpdateTicket(1, ticket);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateTicket_ShouldReturn400_WhenIdMismatch()
        {
            var ticket = new Ticket { Id = 2, Price = 200 };

            var result = await _controller.UpdateTicket(1, ticket);

            var bad = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Ticket ID mismatch.", bad.Value);
        }

        [Fact]
        public async Task UpdateTicket_ShouldReturn404_WhenNotFound()
        {
            var ticket = new Ticket { Id = 1, Price = 300 };
            _mockRepo.Setup(r => r.update(ticket)).ReturnsAsync((Ticket)null);

            var result = await _controller.UpdateTicket(1, ticket);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteTicket_ShouldReturn204_WhenDeleted()
        {
            var ticket = new Ticket { Id = 1 };
            _mockRepo.Setup(r => r.delete(1)).ReturnsAsync(ticket);

            var result = await _controller.DeleteTicket(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteTicket_ShouldReturn404_WhenNotFound()
        {
            _mockRepo.Setup(r => r.delete(99)).ReturnsAsync((Ticket)null);

            var result = await _controller.DeleteTicket(99);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
