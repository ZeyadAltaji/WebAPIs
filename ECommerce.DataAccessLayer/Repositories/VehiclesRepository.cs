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
    public class VehiclesRepository : IRepository<Vehicles>
    {
        private readonly DBContext Dc;

        public VehiclesRepository(DBContext dc)
        {
            Dc = dc;
        }


        public void Create(Vehicles entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Dc.Vehicle.Add(entity);
            Dc.SaveChanges();
        }

        public void Delete(int Id, Vehicles entity)
        {
            entity.IsDelete = true;
            Dc.Vehicle.Update(entity);
            Dc.SaveChanges();
        }
        public async Task<IEnumerable<Vehicles>> GetAll()
        {
            return await Dc.Vehicle.Include(Getbyid => Getbyid.User).Where(x => x.IsDelete == false).ToListAsync();

        }
        public async Task<Vehicles> GetByID(int id)
        {
            return await Dc.Vehicle.Include(Getbyid => Getbyid.User).SingleOrDefaultAsync(Getbyid => Getbyid.Id == id);
        }

        public void Update(int Id, Vehicles entity)
        {
            Dc.Vehicle.Update(entity);
            Dc.SaveChanges();

        }

        public void Active(int Id, Vehicles entity)
        {
            if (entity.IsActive == false)
            {
                entity.IsActive = true;
            }
            else if (entity.IsActive == true)
            {
                entity.IsActive = false;
            }
            Dc.Vehicle.Update(entity);
            Dc.SaveChanges();
        }

        public IList<Vehicles> GetAllViewFrontClinet()
        {
            return Dc.Vehicle.Include(Getbyid => Getbyid.User).Where(x => x.IsActive == true && x.IsDelete == false).ToList();
        }
 
    }
}
