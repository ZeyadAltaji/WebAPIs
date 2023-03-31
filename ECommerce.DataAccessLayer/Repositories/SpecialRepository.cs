using ECommerce.Application.Abstractions;
using ECommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccessLayer.Repositories
{
    public class SpecialRepository : IRepository<Special>
    {
        public DBContext Dc { get; }

        public SpecialRepository(DBContext dc)
        {
            Dc = dc;
        }


        public void Create(Special entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Dc.Specials.Add(entity);
            Dc.SaveChanges();
        }

        public void Delete(int Id, Special entity)
        {
            entity.IsDelete = true;
            Dc.Specials.Update(entity);
            Dc.SaveChanges();
        }

        public List<Special> GetAll()
        {
            return Dc.Specials.Include(Getbyid => Getbyid.User).Where(x => x.IsDelete == false).ToList();
        }

        public Special GetByID(int id)
        {
            var GetbyIdSpecials = Dc.Specials.Include(Getbyid => Getbyid.User).SingleOrDefault(Getbyid => Getbyid.Id == id);
            //var GetbyIdBrands = Dc.Brand.Include(Getbyid => Getbyid.Id == id).Where(Getbyid => Getbyid.Id == id).SingleOrDefault();
            return GetbyIdSpecials;
        }

        public void Update(int Id, Special entity)
        {
            Dc.Specials.Update(entity);
            Dc.SaveChanges();
        }

        public void Active(int Id, Special entity)
        {
            if (entity.IsActive == false)
            {
                entity.IsActive = true;
            }
            else if (entity.IsActive == true)
            {
                entity.IsActive = false;
            }
            Dc.Specials.Update(entity);
            Dc.SaveChanges();
        }

        public IList<Special> GetAllViewFrontClinet()
        {
            return Dc.Specials.Include(Getbyid => Getbyid.User).Where(x => x.IsActive == true && x.IsDelete == false).ToList();
        }
    }
}
