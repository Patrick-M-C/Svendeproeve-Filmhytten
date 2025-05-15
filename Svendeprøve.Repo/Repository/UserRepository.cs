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
    /*
    * UserRepository
    * 
    * Står for al datatilgang relateret til Users i systemet.
    * Implementerer IUser-interfacet og bruger Entity Framework Core til at håndtere forespørgsler og ændringer i databasen.
    * 
    * Funktionalitet:
    * - Hent alle brugere eller en enkelt bruger baseret på ID, navn eller e-mail.
    * - Understøtter også hentning af brugere med tilknyttede billetter.
    * - Opret, opdater og slet brugere i databasen.
    * 
    */
    public class UserRepository : IUser
    {
        private readonly Databasecontext _context;        

        public UserRepository(Databasecontext context)
        {
            _context = context;           
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<List<User>> GetAllIncludeUserTicketAsync()
        {
            return await _context.User.Include(u => u.Tickets).ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetByNameAsync(string name)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Name == name);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> CreateAsync(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));            

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            var existingUser = await _context.User.FirstOrDefaultAsync(u => u.Id == user.Id);

            if (existingUser == null) throw new KeyNotFoundException("User not found");

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.PhoneNumber = user.PhoneNumber;
            existingUser.Password = user.Password;
            existingUser.IsAdmin = user.IsAdmin;            

            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task<User> DeleteAsync(int id)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return null;

            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
