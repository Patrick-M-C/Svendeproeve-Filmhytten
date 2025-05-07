using Microsoft.EntityFrameworkCore;
using Svendeprøve.Repo.DatabaseContext;
using Svendeprøve.Repo.DTO;
using Svendeprøve.Repo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Svendeprøve.Repo.Repository
{
    public class UserRepository : IUser
    {
        private readonly Databasecontext context;
        //private readonly PasswordHasher<User> _passwordHasher;

        public UserRepository(Databasecontext context)
        {
            context = context;
            //_passwordHasher = new PasswordHasher<User>();
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await context.User.ToListAsync();
        }

        public async Task<List<User>> GetAllIncludeUserTicketAsync()
        {
            return await context.User.Include(u => u.Tickets).ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await context.User.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetByNameAsync(string name)
        {
            return await context.User.FirstOrDefaultAsync(u => u.Name == name);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await context.User.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> CreateAsync(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            //user.Password = _passwordHasher.HashPassword(user, user.Password);

            context.User.Add(user);
            await context.SaveChangesAsync();

            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            var existingUser = await context.User.FirstOrDefaultAsync(u => u.Id == user.Id);

            if (existingUser == null) throw new KeyNotFoundException("User not found");

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.PhoneNumber = user.PhoneNumber;
            //existingUser.Password = user.Password;

            //existingUser.Password = _passwordHasher.HashPassword(existingUser, user.Password);

            await context.SaveChangesAsync();
            return existingUser;
        }

        public async Task<User> DeleteAsync(int id)
        {
            var user = await context.User.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return null;

            context.User.Remove(user);
            await context.SaveChangesAsync();
            return user;
        }
    }
}
