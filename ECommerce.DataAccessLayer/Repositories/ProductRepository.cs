using CloudinaryDotNet;
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
    public class ProductRepository : IRepository<Product>
    {
        private readonly DBContext Dc;


        public ProductRepository(DBContext dc)
        {
            Dc = dc;
        }
        public void Create(Product entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Dc.Products.Add(entity);
            Dc.SaveChanges();
        }

        public void Delete(int Id, Product entity)
        {
            entity.IsDelete = true;
            Dc.Products.Update(entity);
            Dc.SaveChanges();
        }
        public async Task<IEnumerable<Product>> GetAll()
        {
             return await Dc.Products.Include(Getbyid => Getbyid.User).Where(x => x.IsDelete == false).ToListAsync();

        }

        public async Task<Product> GetByID(int id)
        {
            return await Dc.Products.Include(Getbyid => Getbyid.User).SingleOrDefaultAsync(Getbyid => Getbyid.Id == id);
 
        }

        public void Update(int Id, Product entity)
        {
            Dc.Products.Update(entity);
            Dc.SaveChanges();
        }

        public void Active(int Id, Product entity)
        {
            if (entity.IsActive == false)
            {
                entity.IsActive = true;
            }
            else if (entity.IsActive == true)
            {
                entity.IsActive = false;
            }
            Dc.Products.Update(entity);
            Dc.SaveChanges();
        }

        public IList<Product> GetAllViewFrontClinet()
        {
            return Dc.Products.Include(Getbyid => Getbyid.User).Include(Getbyid => Getbyid.Category).Include(Getbyid => Getbyid.Brands).Include(Getbyid => Getbyid.Car).Where(x => x.IsActive == true && x.IsDelete == false).ToList();
        }

       
       
    }
}
