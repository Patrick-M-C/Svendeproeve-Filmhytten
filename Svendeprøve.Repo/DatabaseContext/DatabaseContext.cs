using Microsoft.EntityFrameworkCore;
using Svendeprøve.Repo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Svendeprøve.Repo.DatabaseContext
{
    public class Databasecontext : DbContext
    {
        public Databasecontext(DbContextOptions<Databasecontext> option) : base(option) { }
        public DbSet<Hall> Hall { get; set; }
        public DbSet<Seat> Seat { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
    }
}
