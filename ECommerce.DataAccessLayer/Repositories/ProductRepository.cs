using Azure.Core;
using ECommerce.Application.Abstractions;
using ECommerce.Application.DTOs;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace ECommerce.DataAccessLayer.Repositories
{
    public class ProductRepository : IListImage<Product>, IRepository<Product>,IGetData<Product>
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
            //return await Dc.Products.Include(Getbyid => Getbyid.User).Include(Getbyid => Getbyid.Image1).Where(x => x.IsDelete == false).ToListAsync();

            return await Dc.Products.Include(Getbyid => Getbyid.User).Include(x=>x.Brands).Where(x => x.IsDelete == false).ToListAsync();
        }

        public async Task<Product> GetByID(int id)
        {
            //return await Dc.Products.Include(Getbyid => Getbyid.User).Include(Getbyid=> Getbyid.Image1).Include(Getbyid => Getbyid.Brands).Include(Getbyid => Getbyid.Category).Include(Getbyid => Getbyid.Car).SingleOrDefaultAsync(Getbyid => Getbyid.Id == id);
            return await Dc.Products.Include(Getbyid => Getbyid.User).SingleOrDefaultAsync(Getbyid => Getbyid.Id == id);

        }
        public async Task<List<Product>> GetProductsByBrand(Brands brand)
        {
               return await Dc.Products
             .Where(p => p.BrandsId == brand.Id)
             .ToListAsync();
        }

        public async Task<List<Product>> GetProductsByCars(Car car)
        {
            return await Dc.Products
           .Where(p => p.CarId == car.Id)
           .ToListAsync();
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
            return Dc.Products.Include(Getbyid => Getbyid.User).Where(x => x.IsActive == true && x.IsDelete == false).ToList();

            //return Dc.Products.Include(Getbyid => Getbyid.User).Include(Getbyid => Getbyid.Category).Include(Getbyid => Getbyid.Brands).Include(Getbyid => Getbyid.Car).Where(x => x.IsActive == true && x.IsDelete == false).ToList();
        }

  

        public async Task<Product> EditAsyncTest(int id, object entity, IFormFile PrimaryImage, IFormFile Foreign_Image1, IFormFile Foreign_Image2)
        {
            var product = await Dc.Products.FindAsync(id);

            if (product == null)
            {
                return null;
            }
            var productDTOs = entity as ProductDTOs;

            
            if (!string.IsNullOrEmpty(productDTOs.Title) && productDTOs.Title != product.Title)
            {
                product.Title = productDTOs.Title;
            }

            
            if (productDTOs.offers != 0 && productDTOs.offers != product.offers)
            {
                product.offers = productDTOs.offers;
            }
            if (productDTOs.Brands_Id != 0 && productDTOs.Brands_Id != product.BrandsId)
            {
                product.BrandsId = productDTOs.Brands_Id;
            }
            if (productDTOs.Car_Id != 0 && productDTOs.Car_Id != product.CarId)
            {
                product.CarId = productDTOs.Car_Id;
            }
            if (productDTOs.Category_Id != 0 && productDTOs.Category_Id != product.CategoryId)
            {
                product.CategoryId = productDTOs.Category_Id;
            }           
            if (productDTOs.Admin_Id != 0 && productDTOs.Admin_Id != product.Admin_Id)
            {
                product.Admin_Id = productDTOs.Admin_Id;
            }
            if (PrimaryImage != null)
            {
                // Save the new image 
                var filePath = Path.Combine(@"D:\project fianal\E-commerce\projects\dashboard\src\assets\image\Products", PrimaryImage.FileName);
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await PrimaryImage.CopyToAsync(fileStream);
                }
                var oldFilePath = Path.Combine(@"D:\project fianal\E-commerce\projects\dashboard\src\assets\image\Products", product.IsPrimaryImage);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }

                product.IsPrimaryImage = PrimaryImage.FileName;
                Dc.Entry(product).Property(x => x.IsPrimaryImage).IsModified = true;
            }
            if (Foreign_Image1 != null)
            {
                // Save the new image 
                var filePath = Path.Combine(@"D:\project fianal\E-commerce\projects\dashboard\src\assets\image\Products", Foreign_Image1.FileName);
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Foreign_Image1.CopyToAsync(fileStream);
                }
                var oldFilePath = Path.Combine(@"D:\project fianal\E-commerce\projects\dashboard\src\assets\image\Products", product.IsForeignImage1);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
                product.IsForeignImage1 = Foreign_Image1.FileName;
                Dc.Entry(product).Property(x => x.IsForeignImage1).IsModified = true;
            }
            if (Foreign_Image2 != null)
            {
                // Save the new image 
                var filePath = Path.Combine(@"D:\project fianal\E-commerce\projects\dashboard\src\assets\image\Products", Foreign_Image2.FileName);
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Foreign_Image2.CopyToAsync(fileStream);
                }
                var oldFilePath = Path.Combine(@"D:\project fianal\E-commerce\projects\dashboard\src\assets\image\Products", product.IsForeignImage2);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
                product.IsForeignImage2 = Foreign_Image2.FileName;
                Dc.Entry(product).Property(x => x.IsForeignImage2).IsModified = true;
            }
            product.UpdateDate = DateTimeOffset.Now.LocalDateTime;
            await Dc.SaveChangesAsync();

            return product;
        }

    }
}
