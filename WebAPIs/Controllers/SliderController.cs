using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.UnitOfWork;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIs.Controllers
{
    [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class SliderController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public SliderController (IUnitOfWork uow , IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        // GET: api/<SliderController>
        [HttpGet("AllSlider")]
        public async Task<IActionResult> GetALlSlider ()
        {
            var SliderGetAll = await uow.repositorySlider.GetAll();
            var SliderDtos = mapper.Map<IEnumerable<SliderDTOs>>(SliderGetAll);
            return Ok(SliderDtos);
        }

        // GET api/<SliderController>/5
        [HttpGet("Sliders/{id}")]
        public async Task<IActionResult> GetById (int id)
        {
            var SliderGetByID = await uow.repositorySlider.GetByID(id);
            return Ok(SliderGetByID);
        }

        // POST api/<SliderController>
        [HttpPost]
        public async Task<IActionResult> Createslider ([FromForm] SliderDTOs sliderDTOs)
        {

            var CreateNewSlider = mapper.Map<Slider>(sliderDTOs);
            CreateNewSlider.ImageURl = await SaveImage(sliderDTOs.Image);

            sliderDTOs.CreateDate = DateTimeOffset.Now.LocalDateTime;
            uow.repositorySlider.Create(CreateNewSlider);
            await uow.SaveChanges();
            return StatusCode(201);
        }

        // PUT api/<SliderController>/5
        [HttpPut("Sliders/update")]
        public async Task<IActionResult> UpdateSlider (int id)
        {
            try
            {
                var sliderDtos = new SliderDTOs();
                sliderDtos.Id = id = int.Parse(HttpContext.Request.Form ["id"].ToString());
                var img = HttpContext.Request.Form.Files ["Image"];

                sliderDtos.Title = HttpContext.Request.Form ["Title"].ToString();
                sliderDtos.Description = HttpContext.Request.Form ["Description"].ToString();
                sliderDtos.Button = HttpContext.Request.Form ["Button"].ToString();

                if ( id != sliderDtos.Id )
                    return BadRequest("Update not allowed");
                var SliderFromDb = await uow.repositorySlider.GetByID(id);

                if ( SliderFromDb == null )
                    return BadRequest("Update not allowed");
                if ( id == SliderFromDb.Id )
                {
                    if ( img != null && img.Length > 0 )
                    {
                        // A new image is selected
                        var Update = await uow.RepositorySlider.EditAsyncTest(id , sliderDtos , img);
                        if ( Update != null ) return StatusCode(200);

                    }
                    else
                    {
                        // Preserve the existing image
                        var Update = await uow.RepositorySlider.EditAsyncTest(id , sliderDtos , null);
                        if ( Update != null ) return StatusCode(200);

                    }
                }
            }
            catch ( Exception ex )
            {
                return BadRequest(ex.Message);

            }

            return BadRequest(404);
        }

        // DELETE api/<SliderController>/5
        [HttpDelete("Slider/Delete/{id}")]
        public async Task<IActionResult> DeleteSlider (int id)
        {
            var SliderDelete = await uow.repositorySlider.GetByID(id);

            uow.repositorySlider.Delete(id , SliderDelete);
            await uow.SaveChanges();
            return Ok(id);
        }
        [NonAction]
        public async Task<string> SaveImage (IFormFile imageFile)
        {
            string imagename = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ' , ' ');
            imagename = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
            string imagePath = Path.Combine(@"D:\project fianal\E-commerce\projects\dashboard\src\assets\image\Slider" , imagename);
            using ( var fileStream = new FileStream(imagePath , FileMode.Create) )
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imagename;
        }


    }
}
