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
        private readonly DBContext Dc;

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

        public async Task<IEnumerable<Special>> GetAll()
        {
            return await Dc.Specials.Include(Getbyid => Getbyid.User).Where(x => x.IsDelete == false).ToListAsync();
        }

        public async Task<Special> GetByID(int id)
        {
            return await Dc.Specials.Include(Getbyid => Getbyid.User).SingleOrDefaultAsync(Getbyid => Getbyid.Id == id);

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
