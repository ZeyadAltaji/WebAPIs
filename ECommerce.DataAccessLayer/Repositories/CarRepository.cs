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
    public class CarRepository : ITesting<Car>,IRepository<Car>
    {
        private readonly DBContext Dc;

        public CarRepository(DBContext dc)
        {
            Dc = dc;
        }


        public void Create(Car entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Dc.Cars.Add(entity);
            Dc.SaveChanges();
        }

        public void Delete(int Id, Car entity)
        {
            entity.IsDelete = true;
            Dc.Cars.Update(entity);
            Dc.SaveChanges();
        }
        public async Task<IEnumerable<Car>> GetAll()
        {
            return await Dc.Cars.Include(Getbyid => Getbyid.User).Where(x => x.IsDelete == false).ToListAsync();

        }

        public async Task<Car> GetByID(int id)
        {
            return await Dc.Cars.Include(Getbyid => Getbyid.User).SingleOrDefaultAsync(Getbyid => Getbyid.Id == id);

        }
         

        public void Update(int Id, Car entity)
        {
            Dc.Cars.Update(entity);
            Dc.SaveChanges();
        }

        public void Active(int Id, Car entity)
        {
            if (entity.IsActive == false)
            {
                entity.IsActive = true;
            }
            else if (entity.IsActive == true)
            {
                entity.IsActive = false;
            }
            Dc.Cars.Update(entity);
            Dc.SaveChanges();
        }

        public IList<Car> GetAllViewFrontClinet()
        {
            return Dc.Cars.Include(Getbyid => Getbyid.User).Where(x => x.IsActive == true && x.IsDelete == false).ToList();
        }
        public async Task<Car> EditAsyncTest(int id, object entity, IFormFile img)
        {
            var Query = await Dc.Cars.FirstOrDefaultAsync(x => x.Id == id);
            if (Query == null)
            {
                return null;
            }

            Dc.Attach(Query);
            var carDTOs = entity as CarDTOs;
            Query.Name = carDTOs.Name;
            Query.Production_Date=carDTOs.Production_Date;
            Query.Class= carDTOs.Class;
            Query.UserUpdate = carDTOs.UserUpdate;
 
            Query.UpdateDate = DateTimeOffset.Now.LocalDateTime;
            if (img != null)
            {
                // Save the new image
                var filePath = Path.Combine(@"D:\project fianal\E-commerce\projects\dashboard\src\assets\image\Cars", img.FileName);
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await img.CopyToAsync(fileStream);
                }
                var oldFilePath = Path.Combine(@"D:\project fianal\E-commerce\projects\dashboard\src\assets\image\Cars", Query.Public_id);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
                Query.Public_id = img.FileName;
                Dc.Entry(Query).Property(x => x.Public_id).IsModified = true;
            }
            Dc.Entry(Query).Property(x => x.Name).IsModified = true;
            Dc.Entry(Query).Property(x => x.UserUpdate).IsModified = true;

            await Dc.SaveChangesAsync();
            return Query;
        }
    }
}
