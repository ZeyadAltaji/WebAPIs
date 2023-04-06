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
        private readonly DBContext Dc;

        public CarRepository(DBContext dc)
        {
            Dc = dc;
        }


        public void Create(Car entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Dc.Cars.Add(entity);
            Dc.SaveChanges();
        }

        public void Delete(int Id, Car entity)
        {
            entity.IsDelete = true;
            Dc.Cars.Update(entity);
            Dc.SaveChanges();
        }
        public async Task<IEnumerable<Car>> GetAll()
        {
            return await Dc.Cars.Include(Getbyid => Getbyid.User).Where(x => x.IsDelete == false).ToListAsync();

        }

        public async Task<Car> GetByID(int id)
        {
            return await Dc.Cars.Include(Getbyid => Getbyid.User).SingleOrDefaultAsync(Getbyid => Getbyid.Id == id);

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
