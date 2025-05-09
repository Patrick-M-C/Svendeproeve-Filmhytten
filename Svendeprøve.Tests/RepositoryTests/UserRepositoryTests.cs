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
    public class UserRepositoryTests
    {
        private readonly DbContextOptions<Databasecontext> _options;
        private readonly Databasecontext _context;
        private readonly UserRepository _userRepo;

        public UserRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<Databasecontext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new Databasecontext(_options);
            _userRepo = new UserRepository(_context);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllUsers()
        {
            await _context.Database.EnsureDeletedAsync();
            _context.User.AddRange(
                new User { Name = "Alice", Email = "a@mail.com", PhoneNumber = "12341234", Password = "Password1" },
                new User { Name = "kap", Email = "b@mail.com", PhoneNumber = "12121212", Password = "Password2" });
            await _context.SaveChangesAsync();

            var result = await _userRepo.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetAllIncludeUserTicketAsync_ShouldIncludeTickets()
        {
            await _context.Database.EnsureDeletedAsync();
            var user = new User
            {
                Name = "Test",
                Email = "test@mail.com",
                PhoneNumber = "000",
                Password = "pw",
                Tickets = new List<Ticket> { new Ticket { Price = 100, IsCanceled = false } }
            };
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            var result = await _userRepo.GetAllIncludeUserTicketAsync();

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Single(result[0].Tickets);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnUser_WhenExists()
        {
            await _context.Database.EnsureDeletedAsync();
            var user = new User { Name = "Charlie", Email = "charlie@mail.com", PhoneNumber = "321", Password = "pw" };
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            var result = await _userRepo.GetByIdAsync(user.Id);

            Assert.NotNull(result);
            Assert.Equal("Charlie", result.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotFound()
        {
            await _context.Database.EnsureDeletedAsync();

            var result = await _userRepo.GetByIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetByNameAsync_ShouldReturnUser_WhenExists()
        {
            await _context.Database.EnsureDeletedAsync();
            var user = new User { Name = "David", Email = "d@mail.com", PhoneNumber = "000", Password = "pw" };
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            var result = await _userRepo.GetByNameAsync("David");

            Assert.NotNull(result);
            Assert.Equal("d@mail.com", result.Email);
        }

        [Fact]
        public async Task GetByEmailAsync_ShouldReturnUser_WhenExists()
        {
            await _context.Database.EnsureDeletedAsync();
            var user = new User { Name = "Ella", Email = "e@mail.com", PhoneNumber = "111", Password = "pw" };
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            var result = await _userRepo.GetByEmailAsync("e@mail.com");

            Assert.NotNull(result);
            Assert.Equal("Ella", result.Name);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddUser()
        {
            await _context.Database.EnsureDeletedAsync();
            var user = new User { Name = "Fred", Email = "f@mail.com", PhoneNumber = "222", Password = "pw" };

            var result = await _userRepo.CreateAsync(user);

            Assert.NotNull(result);
            Assert.True(result.Id > 0);
            Assert.Equal("Fred", result.Name);
        }

        [Fact]
        public async Task CreateAsync_ShouldThrow_WhenUserIsNull()
        {
            await _context.Database.EnsureDeletedAsync();

            await Assert.ThrowsAsync<ArgumentNullException>(() => _userRepo.CreateAsync(null));
        }

        [Fact]
        public async Task UpdateAsync_ShouldModifyUser_WhenExists()
        {
            await _context.Database.EnsureDeletedAsync();
            var user = new User { Name = "George", Email = "g@mail.com", PhoneNumber = "333", Password = "pw" };
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            user.Name = "Greg";
            user.Email = "greg@mail.com";
            var result = await _userRepo.UpdateAsync(user);

            Assert.NotNull(result);
            Assert.Equal("Greg", result.Name);
            Assert.Equal("greg@mail.com", result.Email);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrow_WhenUserNotFound()
        {
            await _context.Database.EnsureDeletedAsync();
            var user = new User { Id = 999, Name = "Ghost", Email = "ghost@mail.com", PhoneNumber = "000", Password = "pw" };

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _userRepo.UpdateAsync(user));
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveUser_WhenExists()
        {
            await _context.Database.EnsureDeletedAsync();
            var user = new User { Name = "Helen", Email = "h@mail.com", PhoneNumber = "444", Password = "pw" };
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            var result = await _userRepo.DeleteAsync(user.Id);

            Assert.NotNull(result);
            var fromDb = await _context.User.FindAsync(user.Id);
            Assert.Null(fromDb);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnNull_WhenNotFound()
        {
            await _context.Database.EnsureDeletedAsync();

            var result = await _userRepo.DeleteAsync(999);

            Assert.Null(result);
        }
    }
}
