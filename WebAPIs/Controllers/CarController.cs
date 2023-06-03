using AutoMapper;
using ECommerce.Application.Abstractions;
using ECommerce.Application.DTOs;
using ECommerce.Application.ImageDTOs;
using ECommerce.Application.UnitOfWork;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WebAPIs.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly IPhotoService photoService;

        public CarController(IUnitOfWork uow, IMapper mapper, IPhotoService photoService)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.photoService = photoService;

        }
        // GET: api/Car/GetAllCar
        [HttpGet("GetAllCar")]
        public async Task<IActionResult> GetAllCar()
        {
            var AllCar = await uow.repositoryCar.GetAll();
            var CarDTOs = mapper.Map<IEnumerable<CarDTOs>>(AllCar);
            return Ok(CarDTOs);

        }
        // GET: api/Car/Cars/id
        [HttpGet("Cars/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var CarsByID = await uow.repositoryCar.GetByID(id);
            return Ok(CarsByID);
        }

        // POST api/Car
        [HttpPost]
        public async Task<IActionResult> CreateCar([FromForm] CarDTOs carDTOs)
        {
            var CreateNewCar = mapper.Map<Car>(carDTOs);
            CreateNewCar.Public_id = await SaveImage(carDTOs.Image_CarUrl);
            CreateNewCar.Production_Date = carDTOs.Production_Date ;

            carDTOs.CreateDate = DateTimeOffset.Now.LocalDateTime;
            uow.repositoryCar.Create(CreateNewCar);
            await uow.SaveChanges();
            return StatusCode(201);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateCar()
        {
            try
            {

                 int id = int.Parse(HttpContext.Request.Form["id"].ToString());
                var img = HttpContext.Request.Form.Files["Image_CarUrl"];
                var carDTOs = new CarDTOs();
                carDTOs.Name = HttpContext.Request.Form["Name"].ToString();
                carDTOs.Production_Date =int.Parse(HttpContext.Request.Form["Production_Date"].ToString());
                carDTOs.Class = HttpContext.Request.Form["Class"].ToString();
                carDTOs.UserUpdate = HttpContext.Request.Form["userUpdate"].ToString();
                carDTOs.UpdateDate = DateTimeOffset.Now.LocalDateTime;
                 if (!StringValues.IsNullOrEmpty(HttpContext.Request.Form["isActive"]))
                {
                    carDTOs.IsActive = bool.Parse(HttpContext.Request.Form["isActive"]);
                }
                if (!string.IsNullOrEmpty(carDTOs.Name))
                {
                    if (img != null && img.Length > 0)
                    {
                        // A new image is selected
                        var Update = await uow.repositoryCars.EditAsyncTest(id, carDTOs, img);
                        if (Update != null) return Ok();
                    }
                    else
                    {
                        // Preserve the existing image
                        var Update = await uow.repositoryCars.EditAsyncTest(id, carDTOs, null);
                        if (Update != null) return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest();
        }
        
        // DELETE api/Car/delete/5
        [HttpPut("Cars/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var CarsDelete = await uow.repositoryCar.GetByID(id);

            uow.repositoryCar.Delete(id, CarsDelete);
            await uow.SaveChanges();
            return Ok(id);
        }
        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imagename = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', ' ');
            imagename = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
            string imagePath = Path.Combine(@"D:\project fianal\E-commerce\projects\dashboard\src\assets\image\Cars", imagename);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imagename;
        }
    }
}
