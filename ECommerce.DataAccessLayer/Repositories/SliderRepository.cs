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
    public class SliderRepository : IRepository<Slider>
    {
        public DBContext Dc { get; }

        public SliderRepository(DBContext dc)
        {
            Dc = dc;
        }


        public void Create(Slider entity)
        {
            Dc.Sliders.Add(entity);
            Dc.SaveChanges();
        }

        public void Delete(int Id, Slider entity)
        {
            entity.IsDelete = true;
            Dc.Sliders.Update(entity);
            Dc.SaveChanges();
        }

        public List<Slider> GetAll()
        {
            return Dc.Sliders.Include(Getbyid => Getbyid.User).Where(x => x.IsDelete == false).ToList();
        }

        public Slider GetByID(int id)
        {
            var GetbyIdSliders = Dc.Sliders.Include(Getbyid => Getbyid.User).SingleOrDefault(Getbyid => Getbyid.Id == id);
            //var GetbyIdBrands = Dc.Brand.Include(Getbyid => Getbyid.Id == id).Where(Getbyid => Getbyid.Id == id).SingleOrDefault();
            return GetbyIdSliders;
        }

        public void Update(int Id, Slider entity)
        {
            Dc.Sliders.Update(entity);
            Dc.SaveChanges();
        }

        public void Active(int Id, Slider entity)
        {
            if (entity.IsActive == false)
            {
                entity.IsActive = true;
            }
            else if (entity.IsActive == true)
            {
                entity.IsActive = false;
            }
            Dc.Sliders.Update(entity);
            Dc.SaveChanges();
        }

        public IList<Slider> GetAllViewFrontClinet()
        {
            return Dc.Sliders.Include(Getbyid => Getbyid.User).Where(x => x.IsActive == true && x.IsDelete == false).ToList();

        }
    }
}
