using ECommerce.Application.Abstractions;
using ECommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;
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
            Dc.Cars.Add(entity);
            Dc.SaveChanges();
        }

        public void Delete(int Id, Car entity)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll()
        {
            return Dc.Cars.ToList();

        }

        public Car GetByID(int id)
        {
            var GetbyIdCars = Dc.Cars.Include(Getbyid => Getbyid.Id == id).SingleOrDefault(Getbyid => Getbyid.Id == id);
            //var GetbyIdBrands = Dc.Brand.Include(Getbyid => Getbyid.Id == id).Where(Getbyid => Getbyid.Id == id).SingleOrDefault();
            return GetbyIdCars;
        }

        public void Update(int Id, Car entity)
        {
            Dc.Cars.Update(entity);
            Dc.SaveChanges();
        }
    }
}
