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
    public class SpecialController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public SpecialController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        // GET: api/<SpecialController>
        [HttpGet("AllSpecial")]
        public async Task<IActionResult> GetALlSpecial()
        {
            var SpecialGetAll = await uow.repositorySpecial.GetAll();
            var SpecialDtos = mapper.Map<IEnumerable<SpecialDTOs>>(SpecialGetAll) ;
            return Ok(SpecialDtos);

        }

        // GET api/<SpecialController>/5
        [HttpGet("Special/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var SpecialGetByID = await uow.repositorySpecial.GetByID(id);
            return Ok(SpecialGetByID);
        }

        // POST api/<SpecialController>
        [HttpPost]
        public async Task<IActionResult> CreateSpecial(SpecialDTOs specialDTOs)
        {
            var CreateNewSpecial = mapper.Map<Special>(specialDTOs);
            specialDTOs.CreateDate = DateTime.Now;
            uow.repositorySpecial.Create(CreateNewSpecial);
            await uow.SaveChanges();
            return StatusCode(201);
        }

        // PUT api/<SpecialController>/5
        [HttpPut("Special/update/{id}")]
        public async Task<IActionResult> UpdateSpecial(int id, SpecialDTOs specialDTOs)
        {
            if (id != specialDTOs.Id)
                return BadRequest("Update not allowed");
            var SpecialFromDb = await uow.repositorySpecial.GetByID(id);

            if (SpecialFromDb == null)
                return BadRequest("Update not allowed");
            mapper.Map(specialDTOs, SpecialFromDb);

            await uow.SaveChanges();
            return StatusCode(200);
        }

        // DELETE api/<SpecialController>/5
        [HttpDelete("Special/Delete/{id}")]
        public async Task<IActionResult> DeleteSpecial(int id)
        {
            var SpecialDelete = await uow.repositorySpecial.GetByID(id);

            uow.repositorySpecial.Delete(id, SpecialDelete);
            await uow.SaveChanges();
            return Ok(id);
        }
    }
    
}
