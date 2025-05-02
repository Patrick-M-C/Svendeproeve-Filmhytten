using Svendeprøve.Repo.DTO;
using Svendeprøve.Repo.Interface;
using Svendeprøve.Repo.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Svendeprøve.Repo.Repository
{
    public class HallRepository : IHall
    {
        Databasecontext context;
        public HallRepository(Databasecontext temp)
        {
            context = temp;
        }
        public Task<Hall> create(Hall hall)
        {
            throw new NotImplementedException();
        }

        public Task<Hall> delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Hall>> getAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<Hall>> getAllIncludeSeats()
        {
            throw new NotImplementedException();
        }

        public Task<Hall> getById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Hall> update(Hall updateHall)
        {
            throw new NotImplementedException();
        }
    }
}
