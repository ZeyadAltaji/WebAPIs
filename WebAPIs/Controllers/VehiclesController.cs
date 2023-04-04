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
    public class VehiclesController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public VehiclesController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        // GET: api/<VehiclesController>
        [HttpGet("AllVehicles")]
        public async Task<IActionResult> GetALlProduct()
        {
            var VehiclesGetAll = await uow.repositoryVehicles.GetAll();
            var VehiclesDtos = mapper.Map<IEnumerable<VehiclesDTOs>>(VehiclesGetAll);
            return Ok(VehiclesDtos);

        }

        // GET api/<VehiclesController>/5
        [HttpGet("Vehicle/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var VehiclesGetByID = await uow.repositoryVehicles.GetByID(id);
            return Ok(VehiclesGetByID);
        }

        // POST api/<VehiclesController>
        [HttpPost]
        public async Task<IActionResult> CreateVehicles(VehiclesDTOs vehiclesDTOs)
        {
            var CreateNewVehicles = mapper.Map<Vehicles>(vehiclesDTOs);
            vehiclesDTOs.CreateDate = DateTime.Now;
            uow.repositoryVehicles.Create(CreateNewVehicles);
            await uow.SaveChanges();
            return StatusCode(201);
        }

        // PUT api/<VehiclesController>/5
        [HttpPut("Vehicle/update/{id}")]
        public async Task<IActionResult> UpdateVehicles(int id, VehiclesDTOs vehiclesDTOs)
        {
            if (id != vehiclesDTOs.Id)
                return BadRequest("Update not allowed");
            var VehiclesFromDb = await uow.repositoryVehicles.GetByID(id);

            if (VehiclesFromDb == null)
                return BadRequest("Update not allowed");
            mapper.Map(vehiclesDTOs, VehiclesFromDb);

            await uow.SaveChanges();
            return StatusCode(200);
        }


        // DELETE api/<VehiclesController>/5
        [HttpDelete("Vehicle/Delete/{id}")]
        public async Task<IActionResult> DeleteVehicles(int id)
        {
            var VehiclesDelete = await uow.repositoryVehicles.GetByID(id);

            uow.repositoryVehicles.Delete(id, VehiclesDelete);
            await uow.SaveChanges();
            return Ok(id);
        }
    }
}
