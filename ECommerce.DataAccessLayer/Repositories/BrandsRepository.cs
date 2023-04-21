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
    public class BrandsRepository : IRepository<Brands>
    {
        private readonly DBContext Dc;

        public BrandsRepository(DBContext dc)
        {
            Dc = dc;
        }
        public void Create(Brands entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Dc.Brand.Add(entity);
            Dc.SaveChanges();
        }

        public void Delete(int Id, Brands entity)
        {
            entity.IsDelete = true;
            Dc.Brand.Update(entity);
            Dc.SaveChanges();
        }
        public async Task<IEnumerable<Brands>> GetAll()
        {
            return await Dc.Brand.Include(Getbyid => Getbyid.User).Where(x => x.IsDelete == false).ToListAsync();

        }

        public async Task<Brands> GetByID(int id)
        {
            var Brand = await Dc.Brand.Include(Getbyid => Getbyid.User).SingleOrDefaultAsync(Getbyid => Getbyid.Id == id);
            return Brand;
        }

        public void Update(int Id, Brands entity)
        {
            Dc.Brand.Update(entity);
            Dc.SaveChanges();

        }

        public void Active(int Id, Brands entity)
        {
            if (entity.IsActive == false)
            {
                entity.IsActive= true;
            }else if(entity.IsActive == true)
            {
                entity.IsActive= false;
            }
            Dc.Brand.Update(entity);
            Dc.SaveChanges();
        }

        public IList<Brands> GetAllViewFrontClinet()
        {
            return Dc.Brand.Include(Getbyid => Getbyid.User).Where(x => x.IsActive == true && x.IsDelete == false).ToList();
        }

     
    }
}
