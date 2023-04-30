using ECommerce.Application.Abstractions;
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
    public class SettingRepository : IRepository<PhotoLogo>, ITesting<PhotoLogo>
    {
        private readonly DBContext Dc;

        public SettingRepository(DBContext dc)
        {
            Dc = dc;
        }

        public void Active(int Id, PhotoLogo entity)
        {
            if (entity.IsActive == false)
            {
                entity.IsActive = true;
            }
            else if (entity.IsActive == true)
            {
                entity.IsActive = false;
            }
            Dc.PhotoLogo.Update(entity);
            Dc.SaveChanges();
        }

        public void Create(PhotoLogo entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Dc.PhotoLogo.Add(entity);
            Dc.SaveChanges();
        }

        public void Delete(int Id, PhotoLogo entity)
        {
            entity.IsDelete = true;
            Dc.PhotoLogo.Update(entity);
            Dc.SaveChanges();
        }

        public async Task<PhotoLogo> EditAsyncTest(int id, object entity, IFormFile img)
        {
            var Query = await Dc.PhotoLogo.FirstOrDefaultAsync(x => x.Id == id);
            if (Query == null)
            {
                return null;
            }
            Dc.Attach(Query);
            if (img != null)
            {
                // Save the new image
                var filePath = Path.Combine(@"D:\project fianal\E-commerce\projects\dashboard\src\assets\image\Setting\Logo", img.FileName);
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await img.CopyToAsync(fileStream);
                }
                var oldFilePath = Path.Combine(@"D:\project fianal\E-commerce\projects\dashboard\src\assets\image\Setting\Logo", Query.IsLogoUrl);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
                Query.IsLogoUrl = img.FileName;
                Dc.Entry(Query).Property(x => x.IsLogoUrl).IsModified = true;
            }
            Dc.Entry(Query).Property(x => x.IsLogoUrl).IsModified = true;
            await Dc.SaveChangesAsync();
            return Query;
        }

        public async Task<IEnumerable<PhotoLogo>> GetAll()
        {
            return await Dc.PhotoLogo.Where(x => x.IsDelete == false).ToListAsync();

        }

        public IList<PhotoLogo> GetAllViewFrontClinet()
        {
            throw new NotImplementedException();
        }

        public async Task<PhotoLogo> GetByID(int id)
        {
            return await Dc.PhotoLogo.SingleOrDefaultAsync(Getbyid => Getbyid.Id == 4);
        }

        public void Update(int Id, PhotoLogo entity)
        {
            throw new NotImplementedException();
        }
    }
}
