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
    public class SubProductRepository: IRepository<SubProducts>, ITesting<SubProducts>
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
            Dc.SubProducts.Update(entity);
            Dc.SaveChanges();
        }

        public void Create(SubProducts entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Dc.SubProducts.Add(entity);
            Dc.SaveChanges();
        }

        public void Delete(int Id, SubProducts entity)
        {
            entity.IsDelete = true;
            Dc.SubProducts.Update(entity);
            Dc.SaveChanges();
        }

        public async Task<SubProducts> EditAsyncTest(int id, object entity, IFormFile img)
        {
            var Query = await Dc.SubProducts.FirstOrDefaultAsync(x => x.Id == id);
            if (Query == null)
            {
                return null;
            }

            Dc.Attach(Query);
            var brandsDTOs = entity as ListSubProducts;
            Query.Title = brandsDTOs.Title;
            Query.UpdateDate = DateTimeOffset.Now.LocalDateTime;

            if (img != null)
            {
                // Save the new image
                var filePath = Path.Combine(@"D:\project fianal\E-commerce\projects\dashboard\src\assets\image\Brands", img.FileName);
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await img.CopyToAsync(fileStream);
                }
                Query.IsPrimaryImage = img.FileName;
                Dc.Entry(Query).Property(x => x.IsPrimaryImage).IsModified = true;
            }
            Dc.Entry(Query).Property(x => x.IsPrimaryImage).IsModified = true;
            await Dc.SaveChangesAsync();
            return Query;
        }

        public async Task<IEnumerable<SubProducts>> GetAll()
        {
            return await Dc.SubProducts.Include(Getbyid => Getbyid.User).Where(x => x.IsDelete == false).ToListAsync();
        }

        public IList<SubProducts> GetAllViewFrontClinet()
        {
            return Dc.SubProducts.Include(Getbyid => Getbyid.User).Where(x => x.IsActive == true && x.IsDelete == false).ToList();

        }

        public async Task<SubProducts> GetByID(int id)
        {
            return await Dc.SubProducts.Include(Getbyid => Getbyid.User).SingleOrDefaultAsync(Getbyid => Getbyid.Id == id);

        }

        public void Update(int Id, SubProducts entity)
        {
            throw new NotImplementedException();
        }
    }
}
