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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ECommerce.DataAccessLayer.Repositories
{
    public class SliderRepository : IRepository<Slider>,  ITesting<Slider>
    {
        private readonly DBContext Dc;

        public SliderRepository(DBContext dc)
        {
            Dc = dc;
        }


        public void Create(Slider entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Dc.Sliders.Add(entity);
            Dc.SaveChanges();
        }

        public void Delete(int Id, Slider entity)
        {
            entity.IsDelete = true;
            Dc.Sliders.Update(entity);
            Dc.SaveChanges();
        }
        public async Task<IEnumerable<Slider>> GetAll()
        {
            return await Dc.Sliders.Include(Getbyid => Getbyid.User).Where(x => x.IsDelete == false).ToListAsync();

        }

        public async Task<Slider> GetByID(int id)
        {
            return await Dc.Sliders.Include(Getbyid => Getbyid.User).SingleOrDefaultAsync(Getbyid => Getbyid.Id == id);

        }

        public void Update(int Id, Slider entity)
        {
            Dc.Sliders.Update(entity);
            Dc.SaveChanges();
        }

        public void Active(int Id, Slider entity)
        {
            if (entity.IsActive == false)
            {
                entity.IsActive = true;
            }
            else if (entity.IsActive == true)
            {
                entity.IsActive = false;
            }
            Dc.Sliders.Update(entity);
            Dc.SaveChanges();
        }

        public IList<Slider> GetAllViewFrontClinet()
        {
            return Dc.Sliders.Include(Getbyid => Getbyid.User).Where(x => x.IsActive == true && x.IsDelete == false).ToList();

        }

        public async Task<Slider> EditAsyncTest(int id, object entity, IFormFile img)
        {
            var Slider = await Dc.Sliders.FindAsync(id);

            if (Slider == null)
            {
                return null;
            }
            var SliderDTOs = entity as SliderDTOs;
            if (!string.IsNullOrEmpty(SliderDTOs.Title) && SliderDTOs.Title != Slider.Title)
            {
                Slider.Title = SliderDTOs.Title;
            }

            if (!string.IsNullOrEmpty(SliderDTOs.Description) && SliderDTOs.Description != Slider.Description)
            {
                Slider.Description = SliderDTOs.Description;
            }
            if (!string.IsNullOrEmpty(SliderDTOs.Button) && SliderDTOs.Button != Slider.Button)
            {
                Slider.Button = SliderDTOs.Button;
            }
            if (img != null)
            {
                // Save the new image 
                var filePath = Path.Combine(@"D:\project fianal\E-commerce\projects\dashboard\src\assets\image\Slider", img.FileName);
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await img.CopyToAsync(fileStream);
                }
                var oldFilePath = Path.Combine(@"D:\project fianal\E-commerce\projects\dashboard\src\assets\image\Slider", Slider.ImageURl);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
                Slider.ImageURl = img.FileName;
                Dc.Entry(Slider).Property(x => x.ImageURl).IsModified = true;
            }

            Slider.UpdateDate = DateTimeOffset.Now.LocalDateTime;
            await Dc.SaveChangesAsync();

            return Slider;
        }
    }
}
