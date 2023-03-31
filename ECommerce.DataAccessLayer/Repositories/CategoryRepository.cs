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
        public DBContext Dc { get; }

        public CategoryRepository(DBContext dc)
        {
            Dc = dc;
        }


        public void Create(Category entity)
        {
            Dc.Categories.Add(entity);
            Dc.SaveChanges();
        }

        public void Delete(int Id, Category entity)
        {
            entity.IsDelete = true;
            Dc.Categories.Update(entity);
            Dc.SaveChanges();
        }

        public List<Category> GetAll()
        {
            return Dc.Categories.Include(Getbyid => Getbyid.User).Where(x => x.IsDelete == false).ToList();
        }

        public Category GetByID(int id)
        {
            var GetbyIdCategory = Dc.Categories.Include(Getbyid => Getbyid.User).SingleOrDefault(Getbyid => Getbyid.Id == id);
            //var GetbyIdBrands = Dc.Brand.Include(Getbyid => Getbyid.Id == id).Where(Getbyid => Getbyid.Id == id).SingleOrDefault();
            return GetbyIdCategory;
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
