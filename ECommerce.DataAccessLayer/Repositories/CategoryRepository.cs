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
    public class CategoryRepository : IRepository<Category>
    {
        private readonly DBContext Dc;

        public CategoryRepository(DBContext dc)
        {
            Dc = dc;
        }


        public  void Create(Category category)
        {
            if (category == null) throw new ArgumentNullException(nameof(category));
             Dc.Categories.Add(category);
            Dc.SaveChanges();
        }

        public void Delete(int Id,Category category)
        {
            category.IsDelete = true;
            Dc.Categories.Update(category);
            Dc.SaveChanges();
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            //return Dc.Categories.Include(Getbyid => Getbyid.User).Where(x => x.IsDelete == false).ToList();
            return await Dc.Categories.Include(Getbyid => Getbyid.User).Where(x => x.IsDelete == false).ToListAsync();

        }

        public async Task<Category> GetByID(int id)
        {
            return await Dc.Categories.Include(Getbyid => Getbyid.User).SingleOrDefaultAsync(Getbyid => Getbyid.Id == id);
            //var GetbyIdBrands = Dc.Brand.Include(Getbyid => Getbyid.Id == id).Where(Getbyid => Getbyid.Id == id).SingleOrDefault();
            
        }

        public void Update(int Id, Category entity)
        {
            Dc.Categories.Update(entity);
            Dc.SaveChanges();
        }

        public void Active(int Id, Category entity)
        {
            if (entity.IsActive == false)
            {
                entity.IsActive = true;
            }
            else if (entity.IsActive == true)
            {
                entity.IsActive = false;
            }
            Dc.Categories.Update(entity);
            Dc.SaveChanges();
        }

        public IList<Category> GetAllViewFrontClinet()
        {
            return Dc.Categories.Include(Getbyid => Getbyid.User).Where(x => x.IsActive == true && x.IsDelete == false).ToList();
        }

        
    }
}