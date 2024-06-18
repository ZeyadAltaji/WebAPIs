using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.UnitOfWork;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIs.Controllers
{
    [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public SettingController (IUnitOfWork uow , IMapper mapper)
        {
            this.mapper = mapper;
            this.uow = uow;

        }
        [HttpGet("Getlogo")]
        public async Task<IActionResult> GetAllCar ()
        {
            var Logo = await uow.SettingRepository.GetAll();
            var LogoDtos = mapper.Map<IEnumerable<PhotoDTOs>>(Logo);
            return Ok(LogoDtos);

        }
        [HttpPost("new_Logo")]
        public async Task<IActionResult> CreateBrands ([FromForm] PhotoDTOs logoDtos)
        {
            var newLogo = mapper.Map<PhotoLogo>(logoDtos);
            newLogo.IsLogoUrl = await SaveImage(logoDtos.Logoimage);
            newLogo.CreateDate = DateTimeOffset.Now.LocalDateTime;
            uow.SettingRepository.Create(newLogo);
            await uow.SaveChanges();
            return StatusCode(201);
        }
        //[HttpPut("update")]
        //public async Task<IActionResult> UpdateBrand()
        //{
        //    try
        //    {
        //        var photoDTOs = new PhotoDTOs();
        //        int s= 1;
        //        var img = HttpContext.Request.Form.Files["IsLogoUrl"];
        //        var fileName = img.FileName; // get the file name of the uploaded image

        //        if (img != null && img.Length > 0)
        //        {
        //            // A new image is selected
        //            var Update = await uow.settingRepository.EditAsyncTest(s, photoDTOs, img);
        //            if (Update != null) return Ok();
        //        }
        //        else
        //        {
        //            return BadRequest("No image uploaded");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    return BadRequest();
        //}
        [HttpGet("logo/{id}")]
        public async Task<IActionResult> GetById (int id)
        {
            var logoByID = await uow.SettingRepository.GetByID(id);
            return Ok(logoByID);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateLogo (int id)
        {
            try
            {
                var photoDTOs = new PhotoDTOs();

                photoDTOs.Id = id = int.Parse(HttpContext.Request.Form ["id"].ToString());
                var img = HttpContext.Request.Form.Files ["IsLogoUrl"];

                if ( id == photoDTOs.Id )
                {
                    if ( img != null && img.Length > 0 )
                    {
                        // A new image is selected
                        var Update = await uow.settingRepository.EditAsyncTest(id , photoDTOs , img);
                        if ( Update != null ) return Ok();
                    }
                    else
                    {
                        // Preserve the existing image
                        var Update = await uow.settingRepository.EditAsyncTest(id , photoDTOs , null);
                        if ( Update != null ) return Ok();
                    }
                }
            }
            catch ( Exception ex )
            {
                return BadRequest(ex.Message);
            }
            return BadRequest();
        }

        [NonAction]

        public async Task<string> SaveImage (IFormFile imageFile)
        {
            string imagename = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ' , ' ');
            imagename = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
            string imagePath = Path.Combine(@"D:\project fianal\E-commerce\projects\dashboard\src\assets\image\Setting" , imagename);
            using ( var fileStream = new FileStream(imagePath , FileMode.Create) )
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imagename;
        }
    }
}
