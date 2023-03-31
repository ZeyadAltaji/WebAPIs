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
        public DBContext Dc { get; }

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

        public List<Product> GetAll()
        {
            return Dc.Products.Include(Getbyid => Getbyid.User).Include(Getbyid => Getbyid.Category).Include(Getbyid => Getbyid.Brands).Include(Getbyid => Getbyid.Car).Where(x => x.IsDelete == false).ToList();
        }

        public Product GetByID(int id)
        {
            var GetbyIdProducts = Dc.Products.Include(Getbyid => Getbyid.User).Include(Getbyid => Getbyid.Category).Include(Getbyid => Getbyid.Brands).Include(Getbyid => Getbyid.Car).SingleOrDefault(Getbyid => Getbyid.Id == id);
            //var GetbyIdBrands = Dc.Brand.Include(Getbyid => Getbyid.Id == id).Where(Getbyid => Getbyid.Id == id).SingleOrDefault();
            return GetbyIdProducts;
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
