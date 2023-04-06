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
        private readonly DBContext Dc;

        public SliderRepository(DBContext dc)
        {
            Dc = dc;
        }


        public void Create(Slider entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Dc.Sliders.Add(entity);
            Dc.SaveChanges();
        }

        public void Delete(int Id, Slider entity)
        {
            entity.IsDelete = true;
            Dc.Sliders.Update(entity);
            Dc.SaveChanges();
        }
        public async Task<IEnumerable<Slider>> GetAll()
        {
            return await Dc.Sliders.Include(Getbyid => Getbyid.User).Where(x => x.IsDelete == false).ToListAsync();

        }

        public async Task<Slider> GetByID(int id)
        {
            return await Dc.Sliders.Include(Getbyid => Getbyid.User).SingleOrDefaultAsync(Getbyid => Getbyid.Id == id);

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
