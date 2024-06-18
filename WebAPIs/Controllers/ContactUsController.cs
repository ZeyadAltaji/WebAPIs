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
    public class ContactUsController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public ContactUsController (IUnitOfWork uow , IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;

        }
        [HttpGet("Messages")]
        public async Task<IActionResult> GetAllCar ()
        {
            var AllMessages = await uow.repositoryContactUs.GetAll();
            var MessagesDTOs = mapper.Map<IEnumerable<ContactUsDTOs>>(AllMessages);
            return Ok(MessagesDTOs);

        }
        [HttpGet("Messages/{id}")]
        public async Task<IActionResult> GetById (int id)
        {
            var MessagesByID = await uow.repositoryContactUs.GetByID(id);
            return Ok(MessagesByID);
        }
        [HttpPost]
        public async Task<IActionResult> CreateMessages ([FromForm] ContactUsDTOs contactUsDTOs)
        {
            var CreateNewMessages = mapper.Map<ContactUs>(contactUsDTOs);
            CreateNewMessages.Show = true;
            uow.repositoryContactUs.Create(CreateNewMessages);
            await uow.SaveChanges();
            return StatusCode(201);
        }
        [HttpPut("Messages/{id}")]
        public async Task<IActionResult> UpdateMessage (int id , [FromForm] int show)
        {
            var existingMessage = await uow.repositoryContactUs.GetByID(id);
            if ( existingMessage == null )
            {
                return NotFound();
            }
            //if (!StringValues.IsNullOrEmpty(HttpContext.Request.Form["Show"]))
            //{
            //    existingMessage.Show = bool.Parse(HttpContext.Request.Form["Show"]);
            //}
            existingMessage.Show = show != 0; // Convert non-zero values to true, zero to false

            await uow.SaveChanges();

            return NoContent();
        }


    }
}