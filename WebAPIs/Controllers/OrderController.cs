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
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public OrderController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;

        }
        // GET: api/Orders/GetAllOrder
        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetALlOrder()
        {
            var AllOrders= await uow.repositoryOrder.GetAll();
            var OrdesrDTOs = mapper.Map<IEnumerable<OrderDTOs>>(AllOrders);
            return Ok(OrdesrDTOs);

        }
        // GET: api/Order/Orders/id
        [HttpGet("Orders/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var OrdersByID = await uow.repositoryOrder.GetByID(id);
            return Ok(OrdersByID);
        }

        // POST api/Orders
        [HttpPost]
        public async Task<IActionResult> CreateOrders(OrderDTOs orderDTOs)
        {
            var CreateNewOrder = mapper.Map<Order>(orderDTOs);
            orderDTOs.CreateDate = DateTime.Now;
            uow.repositoryOrder.Create(CreateNewOrder);
            await uow.SaveChanges();
            return StatusCode(201);
        }

        // PUT api/Order/update/5
        [HttpPut("Orders/update/{id}")]
        public async Task<IActionResult> UpdateOrders(int id, OrderDTOs orderDTOs)
        {
            if (id != orderDTOs.Id)
                return BadRequest("Update not allowed");
            var OrderFromDb = await uow.repositoryOrder.GetByID(id);

            if (OrderFromDb == null)
                return BadRequest("Update not allowed");
            mapper.Map(orderDTOs, OrderFromDb);

            await uow.SaveChanges();
            return StatusCode(200);
        }

        // DELETE api/Order/delete/5
        [HttpPut("Orders/Delete/{id}")]
        public async Task<IActionResult> DeleteOrders(int id)
        {
            var OrdersDelete = await uow.repositoryOrder.GetByID(id);

            uow.repositoryOrder.Delete(id, OrdersDelete);
            await uow.SaveChanges();
            return Ok(id);
        }
    }
}
