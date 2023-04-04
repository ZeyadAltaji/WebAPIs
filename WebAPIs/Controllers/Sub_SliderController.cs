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
        [HttpGet("Sub_Slider/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Sub_SliderGetByID = await uow.repositorySub_Slider.GetByID(id);
            return Ok(Sub_SliderGetByID);
        }

        // POST api/<Sub_SliderController>
        [HttpPost]
        public async Task<IActionResult> CreateSub_Slider(Sub_SliderDTOs sub_SliderDTOs)
        {
            var CreateNewSub_Slider = mapper.Map<Sub_Slider>(sub_SliderDTOs);
            sub_SliderDTOs.CreateDate = DateTime.Now;
            uow.repositorySub_Slider.Create(CreateNewSub_Slider);
            await uow.SaveChanges();
            return StatusCode(201);
        }

        // PUT api/<Sub_SliderController>/5
        [HttpPut("Sub_Slider/update/{id}")]
        public async Task<IActionResult> UpdateSub_Slider(int id, Sub_SliderDTOs sub_SliderDTOs)
        {
            if (id != sub_SliderDTOs.Id)
                return BadRequest("Update not allowed");
            var Sub_SliderFromDb = await uow.repositorySub_Slider.GetByID(id);

            if (Sub_SliderFromDb == null)
                return BadRequest("Update not allowed");
            mapper.Map(sub_SliderDTOs, Sub_SliderFromDb);

            await uow.SaveChanges();
            return StatusCode(200);
        }

        // DELETE api/<Sub_SliderController>/5
        [HttpDelete("Sub_Slider/Delete/{id}")]
        public async Task<IActionResult> DeleteSub_Slider(int id)
        {
            var Sub_SliderDelete = await uow.repositorySub_Slider.GetByID(id);

            uow.repositorySub_Slider.Delete(id, Sub_SliderDelete);
            await uow.SaveChanges();
            return Ok(id);
        }
    }
}
