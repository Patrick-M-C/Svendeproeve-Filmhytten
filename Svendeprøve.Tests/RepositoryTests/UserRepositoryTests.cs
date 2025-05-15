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
                .UseInMemoryDatabase(databaseName: "UserRepositoryTests")
                .Options;

            _context = new(_options);
            _userRepo = new(_context);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllUsers()
        {
            await _context.Database.EnsureDeletedAsync();

            _context.User.Add(new User { Id = 1, Name = "Alice", Email = "a@mail.com", PhoneNumber = "12341234", Password = "Password1", IsAdmin = true});
            _context.User.Add(new User { Id = 2, Name = "kap", Email = "b@mail.com", PhoneNumber = "12121212", Password = "Password2", IsAdmin = false });
            
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
                Name = "Alice", Email = "test@mail.com", PhoneNumber = "12341234", Password = "Password1", IsAdmin = true, Tickets = new List<Ticket> { new Ticket { Price = 100, IsCanceled = false } }
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
            var user = new User { Name = "Alice", Email = "test@mail.com", PhoneNumber = "12341234", Password = "Password1", IsAdmin = true };
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            var result = await _userRepo.GetByIdAsync(user.Id);

            Assert.NotNull(result);
            Assert.Equal("Alice", result.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotFound()
        {
            await _context.Database.EnsureDeletedAsync();

            var result = await _userRepo.GetByIdAsync(99);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetByNameAsync_ShouldReturnUser_WhenExists()
        {
            await _context.Database.EnsureDeletedAsync();
            var user = new User { Name = "Alice", Email = "test@mail.com", PhoneNumber = "12341234", Password = "Password1", IsAdmin = true };
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            var result = await _userRepo.GetByNameAsync("Alice");

            Assert.NotNull(result);
            Assert.Equal("test@mail.com", result.Email);
        }

        [Fact]
        public async Task GetByEmailAsync_ShouldReturnUser_WhenExists()
        {
            await _context.Database.EnsureDeletedAsync();
            var user = new User { Name = "Alice", Email = "test@mail.com", PhoneNumber = "12341234", Password = "Password1", IsAdmin = true };
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            var result = await _userRepo.GetByEmailAsync("test@mail.com");

            Assert.NotNull(result);
            Assert.Equal("Alice", result.Name);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddUser()
        {
            await _context.Database.EnsureDeletedAsync();
            var user = new User { Name = "Alice", Email = "test@mail.com", PhoneNumber = "12341234", Password = "Password1", IsAdmin = true };

            var result = await _userRepo.CreateAsync(user);

            Assert.NotNull(result);
            Assert.True(result.Id > 0);
            Assert.Equal("Alice", result.Name);
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
            var user = new User { Name = "Alice", Email = "test@mail.com", PhoneNumber = "12341234", Password = "Password1", IsAdmin = true };
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            user.Name = "Kasp";
            user.Email = "kasp@mail.com";
            var result = await _userRepo.UpdateAsync(user);

            Assert.NotNull(result);
            Assert.Equal("Kasp", result.Name);
            Assert.Equal("kasp@mail.com", result.Email);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrow_WhenUserNotFound()
        {
            await _context.Database.EnsureDeletedAsync();
            var user = new User { Id = 99, Name = "testtest", Email = "test1@mail.com", PhoneNumber = "88888887", Password = "passwordtest", IsAdmin = false };

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _userRepo.UpdateAsync(user));
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveUser_WhenExists()
        {
            await _context.Database.EnsureDeletedAsync();
            var user = new User { Name = "Alice", Email = "test@mail.com", PhoneNumber = "12341234", Password = "Password1", IsAdmin = false };
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            var result = await _userRepo.DeleteAsync(user.Id);

            Assert.NotNull(result);
            var data = await _context.User.FindAsync(user.Id);
            Assert.Null(data);
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
