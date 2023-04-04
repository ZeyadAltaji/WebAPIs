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
    public class SliderController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public SliderController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        // GET: api/<SliderController>
        [HttpGet("AllSlider")]
        public async Task<IActionResult> GetALlSlider()
        {
            var SliderGetAll = await uow.repositorySlider.GetAll();
            var SliderDtos = mapper.Map<IEnumerable<SliderDTOs>>(SliderGetAll);
            return Ok(SliderDtos);
        }

        // GET api/<SliderController>/5
        [HttpGet("Slider/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var SliderGetByID = await uow.repositorySlider.GetByID(id);
            return Ok(SliderGetByID);
        }

        // POST api/<SliderController>
        [HttpPost]
        public async Task<IActionResult> Createslider(SliderDTOs sliderDTOs)
        {

            var CreateNewSlider = mapper.Map<Slider>(sliderDTOs);
            sliderDTOs.CreateDate = DateTime.Now;
            uow.repositorySlider.Create(CreateNewSlider);
            await uow.SaveChanges();
            return StatusCode(201);
        }

        // PUT api/<SliderController>/5
        [HttpPut("Slider/update/{id}")]
        public async Task<IActionResult> UpdateSlider(int id, SliderDTOs sliderDTOs)
        {
            if (id != sliderDTOs.Id)
                return BadRequest("Update not allowed");
            var SliderFromDb = await uow.repositorySlider.GetByID(id);

            if (SliderFromDb == null)
                return BadRequest("Update not allowed");
            mapper.Map(sliderDTOs, SliderFromDb);

            await uow.SaveChanges();
            return StatusCode(200);
        }

        // DELETE api/<SliderController>/5
        [HttpDelete("Slider/Delete/{id}")]
        public async Task<IActionResult> DeleteSlider(int id)
        {
            var SliderDelete = await uow.repositorySlider.GetByID(id);

            uow.repositorySlider.Delete(id, SliderDelete);
            await uow.SaveChanges();
            return Ok(id);
        }
    }
}
