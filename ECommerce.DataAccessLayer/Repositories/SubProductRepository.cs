using ECommerce.Application.Abstractions;
using ECommerce.Application.DTOs;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccessLayer.Repositories
{
    public class SubProductRepository: IRepository<SubProducts>, ITesting<SubProducts>,IGetDataByProducts<SubProducts>, IGetById<SubProducts>
    {
        private readonly DBContext Dc;


        public SubProductRepository(DBContext dc)
        {
            Dc = dc;
        }

        public void Active(int Id, SubProducts entity)
        {
            if (entity.IsActive == false)
            {
                entity.IsActive = true;
            }
            else if (entity.IsActive == true)
            {
                entity.IsActive = false;
            }
            Dc.SProducts.Update(entity);
            Dc.SaveChanges();
        }

        public void Create(SubProducts entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Dc.SProducts.Add(entity);
            Dc.SaveChanges();
        }

        public void Delete(int Id, SubProducts entity)
        {
            entity.IsDelete = true;
            Dc.SProducts.Update(entity);
            Dc.SaveChanges();
        }

        public async Task<SubProducts> EditAsyncTest(int id, object entity, IFormFile img)
        {
            var productDTOs = entity as SubProductDTOs;

            var product = await Dc.SProducts.FindAsync(id);

            if (product == null)
            {
                return null;
            }
           

            Dc.Attach(product);
            product.UserUpdate = productDTOs.UserUpdate;

            product.UpdateDate = DateTimeOffset.Now.LocalDateTime;
            if (!string.IsNullOrEmpty(productDTOs.Serial_Id) && productDTOs.Serial_Id != product.Serial_Id)
            {
                product.Serial_Id = productDTOs.Serial_Id;
            }

            if (!string.IsNullOrEmpty(productDTOs.Title) && productDTOs.Title != product.Title)
            {
                product.Title = productDTOs.Title;
            }

            if (!string.IsNullOrEmpty(productDTOs.Description) && productDTOs.Description != product.Description)
            {
                product.Description = productDTOs.Description;
            }

            if (productDTOs.Price != 0 && productDTOs.Price != product.Price)
            {
                product.Price = productDTOs.Price;
            }

            if (productDTOs.offers != 0 && productDTOs.offers != product.offers)
            {
                product.offers = productDTOs.offers;
            }

            if (productDTOs.New_price != 0 && productDTOs.New_price != product.New_price)
            {
                product.New_price = productDTOs.New_price;
            }

            if (productDTOs.Quantity != 0 && productDTOs.Quantity != product.Quantity)
            {
                product.Quantity = productDTOs.Quantity;
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

            if (productDTOs.Customer_Id != 0 && productDTOs.Customer_Id != product.Customer_Id)
            {
                product.Customer_Id = productDTOs.Customer_Id;
            }
            if (productDTOs.productId != 0 && productDTOs.productId != product.productId)
            {
                product.productId = productDTOs.productId;
            }

            if (productDTOs.Admin_Id != 0 && productDTOs.Admin_Id != product.Admin_Id)
            {
                product.Admin_Id = productDTOs.Admin_Id;
            }
            if (img != null)
            {
                // Save the new image
                var filePath = Path.Combine(@"D:\project fianal\E-commerce\projects\dashboard\src\assets\image\SubProduct", img.FileName);
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await img.CopyToAsync(fileStream);
                }
                product.IsPrimaryImage = img.FileName;
                Dc.Entry(product).Property(x => x.IsPrimaryImage).IsModified = true;
            }
            Dc.Entry(product).Property(x => x.IsPrimaryImage).IsModified = true;
            Dc.Entry(product).Property(x => x.UserUpdate).IsModified = true;

            await Dc.SaveChangesAsync();
            return product;
        }

        public async Task<IEnumerable<SubProducts>> GetAll()
        {
            return await Dc.SProducts.Include(Getbyid => Getbyid.User).Where(x => x.IsDelete == false).ToListAsync();
        }

        public async Task<IEnumerable<SubProducts>> GetAllById(int userId)
        {
            return await Dc.SProducts
             .Include(Getbyid => Getbyid.User)
             .Where(x => x.IsDelete == false && x.Admin_Id == userId)
             .ToListAsync();
        }

        public IList<SubProducts> GetAllViewFrontClinet()
        {
            return Dc.SProducts.Include(Getbyid => Getbyid.User).Where(x => x.IsActive == true && x.IsDelete == false).ToList();

        }

        public async Task<SubProducts> GetByID(int id)
        {
            return await Dc.SProducts.Include(Getbyid => Getbyid.User).SingleOrDefaultAsync(Getbyid => Getbyid.Id == id);

        }
        public async Task<List<SubProducts>> GetSubProductsByProducts(Product product)
        {
            // Use Include method to include related SubProducts
            var subProducts = await Dc.SProducts
                .Include(sp => sp.product)
                .Where(sp => sp.productId == product.Id)
                .ToListAsync();

            return subProducts;
        }

        public void Update(int Id, SubProducts entity)
        {
            throw new NotImplementedException();
        }
    }
}
