using ECommerce.Application.Abstractions;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccessLayer.Repositories
{
    public class BrandsRepository : ITesting<Brands>, IRepository<Brands> 
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
            return await Dc.Brand.Include(Getbyid => Getbyid.User).SingleOrDefaultAsync(Getbyid => Getbyid.Id == id);
            
        }

        public void Update(int id, Brands entity)
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
        public async Task<Brands> EditAsync(int id, string Name, IFormFile img)
        {
            var BrandEdit = await Dc.Brand.FirstOrDefaultAsync(x => x.Id == id);
            if (BrandEdit == null)
            {
                return null;
            }

            Dc.Attach(BrandEdit);
            BrandEdit.Name = Name;
            if (img != null)
            {
                // Save the new image
                var filePath = Path.Combine(@"D:\project fianal\E-commerce\projects\dashboard\src\assets\image\Brands", img.FileName);
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await img.CopyToAsync(fileStream);
                }
                BrandEdit.Public_id = img.FileName;
                Dc.Entry(BrandEdit).Property(x => x.Public_id).IsModified = true;
            }
            Dc.Entry(BrandEdit).Property(x => x.Name).IsModified = true;
            await Dc.SaveChangesAsync();
            return BrandEdit;
        }

    }
}
