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
            entity.IsDelete = true;
            Dc.Cars.Update(entity);
            Dc.SaveChanges();
        }

        public List<Car> GetAll()
        {
            return Dc.Cars.Include(Getbyid => Getbyid.User).Where(x => x.IsDelete == false).ToList();

        }

        public Car GetByID(int id)
        {
            var GetbyIdCars = Dc.Cars.Include(Getbyid => Getbyid.User).SingleOrDefault(Getbyid => Getbyid.Id == id);
            //var GetbyIdBrands = Dc.Brand.Include(Getbyid => Getbyid.Id == id).Where(Getbyid => Getbyid.Id == id).SingleOrDefault();
            return GetbyIdCars;
        }

        public void Update(int Id, Car entity)
        {
            Dc.Cars.Update(entity);
            Dc.SaveChanges();
        }

        public void Active(int Id, Car entity)
        {
            if (entity.IsActive == false)
            {
                entity.IsActive = true;
            }
            else if (entity.IsActive == true)
            {
                entity.IsActive = false;
            }
            Dc.Cars.Update(entity);
            Dc.SaveChanges();
        }

        public IList<Car> GetAllViewFrontClinet()
        {
            return Dc.Cars.Include(Getbyid => Getbyid.User).Where(x => x.IsActive == true && x.IsDelete == false).ToList();
        }
    }
}
