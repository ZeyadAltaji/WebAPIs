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
        public DBContext Dc { get; }

        public VehiclesRepository(DBContext dc)
        {
            Dc = dc;
        }


        public void Create(Vehicles entity)
        {
            Dc.Vehicle.Add(entity);
            Dc.SaveChanges();
        }

        public void Delete(int Id, Vehicles entity)
        {
            entity.IsDelete = true;
            Dc.Vehicle.Update(entity);
            Dc.SaveChanges();
        }

        public List<Vehicles> GetAll()
        {
            return Dc.Vehicle.Include(Getbyid => Getbyid.User).Where(x => x.IsDelete == false).ToList();

        }

        public Vehicles GetByID(int id)
        {
            var GetbyIdVehicle = Dc.Vehicle.Include(Getbyid => Getbyid.User).SingleOrDefault(Getbyid => Getbyid.Id == id);
            //var GetbyIdBrands = Dc.Brand.Include(Getbyid => Getbyid.Id == id).Where(Getbyid => Getbyid.Id == id).SingleOrDefault();
            return GetbyIdVehicle;
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
