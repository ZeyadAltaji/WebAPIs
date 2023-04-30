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
    public class Sub_SliderRepository : IRepository<Sub_Slider>, IListImage<Sub_Slider>
    {
        private readonly DBContext Dc;

        public Sub_SliderRepository(DBContext dc)
        {
            Dc = dc;
        }


        public void Create(Sub_Slider entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Dc.SubSliders.Add(entity);
            Dc.SaveChanges();
        }
      

        public void Delete(int Id, Sub_Slider entity)
        {
            entity.IsDelete = true;
            Dc.SubSliders.Update(entity);
            Dc.SaveChanges();
        }
        public async Task<IEnumerable<Sub_Slider>> GetAll()
        {
            return await Dc.SubSliders.Include(Getbyid => Getbyid.User).Where(x => x.IsDelete == false).ToListAsync();

        }
        public async Task<Sub_Slider> GetByID(int id)
        {
            return await Dc.SubSliders.Include(Getbyid => Getbyid.User).SingleOrDefaultAsync(Getbyid => Getbyid.Id == id);
        }

        public void Update(int Id, Sub_Slider entity)
        {
            Dc.SubSliders.Update(entity);
            Dc.SaveChanges();
        }

        public void Active(int Id, Sub_Slider entity)
        {
            if (entity.IsActive == false)
            {
                entity.IsActive = true;
            }
            else if (entity.IsActive == true)
            {
                entity.IsActive = false;
            }
            Dc.SubSliders.Update(entity);
            Dc.SaveChanges();
        }

        public IList<Sub_Slider> GetAllViewFrontClinet()
        {
            return Dc.SubSliders.Include(Getbyid => Getbyid.User).Where(x => x.IsActive == true && x.IsDelete == false).ToList();
        }

        public async Task<Sub_Slider> EditAsyncTest(int id, object entity, IFormFile PrimaryImage, IFormFile Foreign_Image1, IFormFile Foreign_Image2)
        {
            var SubSlider = await Dc.SubSliders.FindAsync(id);

            if (SubSlider == null)
            {
                return null;
            }
            var SubSliderDTOs = entity as Sub_SliderDTOs;
            if (!string.IsNullOrEmpty(SubSliderDTOs.Title) && SubSliderDTOs.Title != SubSlider.Title)
            {
                SubSlider.Title = SubSliderDTOs.Title;
            }

            if (!string.IsNullOrEmpty(SubSliderDTOs.Description) && SubSliderDTOs.Description != SubSlider.Description)
            {
                SubSlider.Description = SubSliderDTOs.Description;
            }
            if (!string.IsNullOrEmpty(SubSliderDTOs.Button) && SubSliderDTOs.Button != SubSlider.Button)
            {
                SubSlider.Button = SubSliderDTOs.Button;
            }
            if (SubSliderDTOs.category_Id != 0 && SubSliderDTOs.category_Id != SubSlider.CategoryId)
            {
                SubSlider.CategoryId = SubSliderDTOs.category_Id;
            }
            if (PrimaryImage != null)
            {
                // Save the new image 
                var filePath = Path.Combine(@"D:\project fianal\E-commerce\projects\dashboard\src\assets\image\SubSlider", PrimaryImage.FileName);
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await PrimaryImage.CopyToAsync(fileStream);
                }
                var oldFilePath = Path.Combine(@"D:\project fianal\E-commerce\projects\dashboard\src\assets\image\SubSlider", SubSlider.ImageURl);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
                SubSlider.ImageURl = PrimaryImage.FileName;
                Dc.Entry(SubSlider).Property(x => x.ImageURl).IsModified = true;
            }
            if (Foreign_Image1 != null)
            {
                // Save the new image 
                var filePath = Path.Combine(@"D:\project fianal\E-commerce\projects\dashboard\src\assets\image\SubSlider", Foreign_Image1.FileName);
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Foreign_Image1.CopyToAsync(fileStream);
                }
                var oldFilePath = Path.Combine(@"D:\project fianal\E-commerce\projects\dashboard\src\assets\image\SubSlider", SubSlider.ImageURl1);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
                SubSlider.ImageURl1 = Foreign_Image1.FileName;
                Dc.Entry(SubSlider).Property(x => x.ImageURl1).IsModified = true;
            }
            if (Foreign_Image2 != null)
            {
                // Save the new image 
                var filePath = Path.Combine(@"D:\project fianal\E-commerce\projects\dashboard\src\assets\image\SubSlider", Foreign_Image2.FileName);
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Foreign_Image2.CopyToAsync(fileStream);
                }
                var oldFilePath = Path.Combine(@"D:\project fianal\E-commerce\projects\dashboard\src\assets\image\SubSlider", SubSlider.ImageURl2);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
                SubSlider.ImageURl2 = Foreign_Image2.FileName;
                Dc.Entry(SubSlider).Property(x => x.ImageURl2).IsModified = true;
            }
            SubSlider.UpdateDate = DateTimeOffset.Now.LocalDateTime;
            await Dc.SaveChangesAsync();

            return SubSlider;
        }

         
            
    }
    
}
