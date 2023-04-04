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
    public class CarController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public CarController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;

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
        public async Task<IActionResult> CreateCar(CarDTOs carDTOs)
        {
            var CreateNewCar = mapper.Map<Car>(carDTOs);
            carDTOs.CreateDate = DateTime.Now;
            uow.repositoryCar.Create(CreateNewCar);
            await uow.SaveChanges();
            return StatusCode(201);
        }

        // PUT api/Car/update/5
        [HttpPut("Cars/update/{id}")]
        public async Task<IActionResult> UpdateCar(int id, CarDTOs carDTOs)
        {
            if (id != carDTOs.Id)
                return BadRequest("Update not allowed");
            var carFromDb = await uow.repositoryCar.GetByID(id);

            if (carFromDb == null)
                return BadRequest("Update not allowed");
            mapper.Map(carDTOs, carFromDb);

            await uow.SaveChanges();
            return StatusCode(200);
        }

        // DELETE api/Car/delete/5
        [HttpPut("Cars/Delete/{id}")]
        public async Task<IActionResult> Deletedelete(int id)
        {
            var CarsDelete = await uow.repositoryCar.GetByID(id);

            uow.repositoryCar.Delete(id, CarsDelete);
            await uow.SaveChanges();
            return Ok(id);
        }
    }
}
