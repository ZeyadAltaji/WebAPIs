using ECommerce.Application.Abstractions;
using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccessLayer.Repositories
{
    public class CarRepository : IRepository<Car>
    {
        public DBContext Dc { get; }

        public CarRepository(DBContext dc)
        {
            Dc = dc;
        }


        public void Create(Car entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int Id, Car entity)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll()
        {
            throw new NotImplementedException();
        }

        public Car GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int Id, Car entity)
        {
            throw new NotImplementedException();
        }
    }
}
