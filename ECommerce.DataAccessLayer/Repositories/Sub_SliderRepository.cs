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
    public class Sub_SliderRepository : IRepository<Sub_Slider>
    {
        public DBContext Dc { get; }

        public Sub_SliderRepository(DBContext dc)
        {
            Dc = dc;
        }


        public void Create(Sub_Slider entity)
        {
            Dc.SubSliders.Add(entity);
            Dc.SaveChanges();
        }

        public void Delete(int Id, Sub_Slider entity)
        {
            entity.IsDelete = true;
            Dc.SubSliders.Update(entity);
            Dc.SaveChanges();
        }

        public List<Sub_Slider> GetAll()
        {
            return Dc.SubSliders.Include(Getbyid => Getbyid.User).Where(x => x.IsDelete == false).ToList();
        }

        public Sub_Slider GetByID(int id)
        {
            var GetbyIdSubSliders = Dc.SubSliders.Include(Getbyid => Getbyid.User).SingleOrDefault(Getbyid => Getbyid.Id == id);
            //var GetbyIdBrands = Dc.Brand.Include(Getbyid => Getbyid.Id == id).Where(Getbyid => Getbyid.Id == id).SingleOrDefault();
            return GetbyIdSubSliders;
        }

        public void Update(int Id, Sub_Slider entity)
        {
            Dc.SubSliders.Update(entity);
            Dc.SaveChanges();
        }

        public void Active(int Id, Sub_Slider entity)
        {
            if (entity.IsActive == false)
            {
                entity.IsActive = true;
            }
            else if (entity.IsActive == true)
            {
                entity.IsActive = false;
            }
            Dc.SubSliders.Update(entity);
            Dc.SaveChanges();
        }

        public IList<Sub_Slider> GetAllViewFrontClinet()
        {
            return Dc.SubSliders.Include(Getbyid => Getbyid.User).Where(x => x.IsActive == true && x.IsDelete == false).ToList();
        }
    }
}
