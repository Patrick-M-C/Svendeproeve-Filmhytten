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
    public class TicketRepositoryTests
    {
        private readonly DbContextOptions<Databasecontext> _options;
        private readonly Databasecontext _context;
        private readonly TicketRepository _ticketRepo;

        public TicketRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<Databasecontext>()
                .UseInMemoryDatabase(databaseName: "TicketRepositoryTests")
                .Options;

            _context = new Databasecontext(_options);
            _ticketRepo = new TicketRepository(_context);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllTickets()
        {
            await _context.Database.EnsureDeletedAsync();

            _context.Ticket.Add(new Ticket
            {
                Price = 100,
                UserId = 1,
                SeatId = 1,
                IsCanceled = false,
            });
            _context.Ticket.Add(new Ticket
            {
                Price = 100,
                UserId = 1,
                SeatId = 1,
                IsCanceled = false,                
            });

            await _context.SaveChangesAsync();

            var result = await _ticketRepo.getAll();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetAllIncludeUserAndSeat_ShouldReturnAllTickets()
        {
            await _context.Database.EnsureDeletedAsync();

            _context.Ticket.Add(new Ticket { Price = 100, UserId = 1, SeatId = 1, IsCanceled = false,
                User = new User
                {
                    Name = "kap",
                    Email = "b@mail.com",
                    PhoneNumber = "12121212",
                    Password = "Password2",
                    IsAdmin = false
                },
                Seat = new Seat
                {
                    Row = 1,
                    SeatNumber = 1,
                    IsReserved = false,
                    HallId = 1
                }
            });
            _context.Ticket.Add(new Ticket
            {
                Price = 100,
                UserId = 1,
                SeatId = 1,
                IsCanceled = false,
                User = new User
                {
                    Name = "Alice",
                    Email = "test@mail.com",
                    PhoneNumber = "12341234",
                    Password = "Password1",
                    IsAdmin = true
                },
                Seat = new Seat
                {
                    Row = 1,
                    SeatNumber = 2,
                    IsReserved = false,
                    HallId = 1
                }
            });

            await _context.SaveChangesAsync();

            var result = await _ticketRepo.getAllIncludeUserAndSeat();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetById_ShouldReturnTicket_WhenExists()
        {
            await _context.Database.EnsureDeletedAsync();
            var ticket = new Ticket { Id = 1, Price = 120, UserId = 1, SeatId = 1, IsCanceled = false };
            _context.Ticket.Add(ticket);
            await _context.SaveChangesAsync();

            var result = await _ticketRepo.getById(ticket.Id);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(120, result.Price);
        }
        
        [Fact]
        public async Task GetByIdIncludeUserAndSeat_ShouldReturnTicket_WhenExists()
        {
            await _context.Database.EnsureDeletedAsync();
            var ticket = new Ticket { Id = 1, Price = 120, UserId = 1, SeatId = 1, IsCanceled = false,
                User = new User
                {
                    Name = "Alice",
                    Email = "test@mail.com",
                    PhoneNumber = "12341234",
                    Password = "Password1",
                    IsAdmin = true
                },
                Seat = new Seat
                {
                    Row = 1,
                    SeatNumber = 2,
                    IsReserved = false,
                    HallId = 1
                }
            };
            _context.Ticket.Add(ticket);
            await _context.SaveChangesAsync();

            var result = await _ticketRepo.getByIdIncludeUserAndSeat(ticket.Id);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(120, result.Price);
            Assert.Equal("Alice", result.User.Name);
        }

        [Fact]
        public async Task GetById_ShouldReturnNull_WhenNotFound()
        {
            await _context.Database.EnsureDeletedAsync();

            var result = await _ticketRepo.getById(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetById_ShouldIncludeSeat_WhenLoadedWithInclude()
        {
            await _context.Database.EnsureDeletedAsync();

            // Arrange: Add Seat and Ticket with foreign key relationship
            var seat = new Seat { Row = 1, SeatNumber = 1, IsReserved = false, HallId = 1 };
            _context.Seat.Add(seat);
            await _context.SaveChangesAsync();

            var ticket = new Ticket
            {
                Price = 100,
                UserId = 1,
                SeatId = seat.Id,
                IsCanceled = false
            };
            _context.Ticket.Add(ticket);
            await _context.SaveChangesAsync();

            // Act: Load ticket with related Seat using Include
            var result = await _context.Ticket
                .Include(t => t.Seat)
                .FirstOrDefaultAsync(t => t.Id == ticket.Id);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Seat);
            Assert.Equal(seat.SeatNumber, result.Seat.SeatNumber);
        }

        [Fact]
        public async Task GetById_ShouldIncludeUser_WhenLoadedWithInclude()
        {
            await _context.Database.EnsureDeletedAsync();

            // Arrange: Add User and Ticket
            var user = new User { Name = "Test User", Email = "test@mail.com", PhoneNumber = "1234", Password = "pw" };
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            var ticket = new Ticket
            {
                Price = 150,
                UserId = user.Id,
                SeatId = 1,
                IsCanceled = false
            };
            _context.Ticket.Add(ticket);
            await _context.SaveChangesAsync();

            // Act: Load ticket with related User using Include
            var result = await _context.Ticket
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == ticket.Id);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.User);
            Assert.Equal("Test User", result.User.Name);
        }



        [Fact]
        public async Task Create_ShouldAddTicket()
        {
            await _context.Database.EnsureDeletedAsync();
            var ticket = new Ticket { Price = 200, UserId = 1, SeatId = 3, IsCanceled = false };

            var result = await _ticketRepo.create(ticket);

            Assert.NotNull(result);
            Assert.True(result.Id > 0);
            Assert.Equal(200, result.Price);
        }

        [Fact]
        public async Task Update_ShouldModifyTicket_WhenExists()
        {
            await _context.Database.EnsureDeletedAsync();
            var ticket = new Ticket { Price = 150, UserId = 1, SeatId = 1, IsCanceled = false };
            _context.Ticket.Add(ticket);
            await _context.SaveChangesAsync();

            ticket.Price = 180;
            ticket.IsCanceled = true;
            var result = await _ticketRepo.update(ticket);

            Assert.NotNull(result);
            Assert.Equal(180, result.Price);
            Assert.True(result.IsCanceled);
        }

        [Fact]
        public async Task Delete_ShouldRemoveTicket_WhenExists()
        {
            await _context.Database.EnsureDeletedAsync();
            var ticket = new Ticket { Price = 130, UserId = 1, SeatId = 1, IsCanceled = false };
            _context.Ticket.Add(ticket);
            await _context.SaveChangesAsync();

            var result = await _ticketRepo.delete(ticket.Id);

            Assert.NotNull(result);
            var fromDb = await _context.Ticket.FindAsync(ticket.Id);
            Assert.Null(fromDb);
        }

        [Fact]
        public async Task Delete_ShouldReturnNull_WhenNotFound()
        {
            await _context.Database.EnsureDeletedAsync();

            var result = await _ticketRepo.delete(999);

            Assert.Null(result);
        }
    }
}
