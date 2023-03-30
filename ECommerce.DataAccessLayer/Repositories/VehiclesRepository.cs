using ECommerce.Application.Abstractions;
using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccessLayer.Repositories
{
    public class VehiclesRepository : IRepository<Vehicles>
    {
        public DBContext Dc { get; }

        public VehiclesRepository(DBContext dc)
        {
            Dc = dc;
        }


        public void Create(Vehicles entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int Id, Vehicles entity)
        {
            throw new NotImplementedException();
        }

        public List<Vehicles> GetAll()
        {
            throw new NotImplementedException();
        }

        public Vehicles GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int Id, Vehicles entity)
        {
            throw new NotImplementedException();
        }
    }
}
