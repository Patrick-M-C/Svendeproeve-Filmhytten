using Svendeprøve.Repo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Svendeprøve.Repo.Interface
{
    public interface IUser
    {
        public Task<List<User>> GetAllAsync();
        public Task<User> GetByIdAsync(int id);
        public Task<User> GetByNameAsync(string name);
        public Task<User> GetByEmailAsync(string email);
        public Task<List<User>> GetAllIncludeUserTicketAsync();
        public Task<User> CreateAsync(User user);
        public Task<User> UpdateAsync(User updateUser);
        public Task<User> DeleteAsync(int id);
    }
}
