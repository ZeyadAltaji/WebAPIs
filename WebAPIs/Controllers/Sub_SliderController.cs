using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.UnitOfWork;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Sub_SliderController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public Sub_SliderController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        // GET: api/<Sub_SliderController>
        [HttpGet("AllSub_Slider")]
        public async Task<IActionResult> GetALlSub_Slider()
        {
            var Sub_SliderGetAll = await uow.repositorySub_Slider.GetAll();
            var Sub_SliderDtos = mapper.Map<IEnumerable<Sub_SliderDTOs>>(Sub_SliderGetAll);
            return Ok(Sub_SliderDtos);

        }

        // GET api/<Sub_SliderController>/5
        [HttpGet("Sub_Sliders/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Sub_SliderGetByID = await uow.repositorySub_Slider.GetByID(id);
            return Ok(Sub_SliderGetByID);
        }

        // POST api/<Sub_SliderController>
        [HttpPost]
        public async Task<IActionResult> CreateSub_Slider([FromForm] Sub_SliderDTOs sub_SliderDTOs)
        {
            var CreateNewSub_Slider = mapper.Map<Sub_Slider>(sub_SliderDTOs);
            CreateNewSub_Slider.ImageURl = await SaveImage(sub_SliderDTOs.Image);
            CreateNewSub_Slider.ImageURl1 = await SaveImage(sub_SliderDTOs.Image1);
            CreateNewSub_Slider.ImageURl2 = await SaveImage(sub_SliderDTOs.Image2);
            sub_SliderDTOs.CreateDate = DateTimeOffset.Now.LocalDateTime;
            uow.repositorySub_Slider.Create(CreateNewSub_Slider);
            await uow.SaveChanges();
            return StatusCode(201);
        }

        // PUT api/<Sub_SliderController>/5
        [HttpPut("Sub_Sliders/update")]
        public async Task<IActionResult> UpdateSub_Slider(int id)
        {
            try
            {
                var subsliderDtos = new Sub_SliderDTOs();
                subsliderDtos.Id = id = int.Parse(HttpContext.Request.Form["id"].ToString());
                var img = HttpContext.Request.Form.Files["Image"];
                var img1 = HttpContext.Request.Form.Files["Image1"];
                var img2 = HttpContext.Request.Form.Files["Image2"];
                subsliderDtos.Title = HttpContext.Request.Form["Title"].ToString();
                subsliderDtos.Description = HttpContext.Request.Form["Description"].ToString();
                subsliderDtos.Button = HttpContext.Request.Form["Button"].ToString();

                if (id != subsliderDtos.Id)
                    return BadRequest("Update not allowed");
                var SliderFromDb = await uow.repositorySlider.GetByID(id);

                if (SliderFromDb == null)
                    return BadRequest("Update not allowed");
                if (id == SliderFromDb.Id)
                {
                    if (img != null && img.Length > 0 || img1 != null && img1.Length > 0)
                    {
                        // A new image is selected
                        var Update = await uow.RepositorySub_Slider.EditAsyncTest(id, subsliderDtos, img, img1, img2);
                        if (Update != null) return StatusCode(200);

                    }
                    else
                    {
                        // Preserve the existing image
                        var Update = await uow.RepositorySub_Slider.EditAsyncTest(id, subsliderDtos, null, null, null);
                        if (Update != null) return StatusCode(200);

                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

            return BadRequest(404);
        }

        // DELETE api/<Sub_SliderController>/5
        [HttpDelete("Sub_Sliders/Delete/{id}")]
        public async Task<IActionResult> DeleteSub_Slider(int id)
        {
            var Sub_SliderDelete = await uow.repositorySub_Slider.GetByID(id);

            uow.repositorySub_Slider.Delete(id, Sub_SliderDelete);
            await uow.SaveChanges();
            return Ok(id);
        }
        [NonAction]
       
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imagename = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', ' ');
            imagename = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
            string imagePath = Path.Combine(@"D:\project fianal\E-commerce\projects\dashboard\src\assets\image\SubSlider", imagename);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imagename;
        }
        
    }
}
