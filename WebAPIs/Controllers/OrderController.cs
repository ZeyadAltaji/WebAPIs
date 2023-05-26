using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.UnitOfWork;
using ECommerce.DataAccessLayer.Repositories;
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
            var AllOrders= await uow.OrderRepository.GetAll();
            var OrdesrDTOs = mapper.Map<IEnumerable<OrderDTOs>>(AllOrders);
            return Ok(OrdesrDTOs);

        }
        // GET: api/Order/Orders/id
        [HttpGet("Orders/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var OrdersByID = await uow.OrderRepository.GetOrderById(id);
            return Ok(OrdersByID);
        }

        // POST api/Orders
        [HttpPost]
        public async Task<IActionResult> CreateOrders(OrderDTOs orderDTOs)
        {
            var CreateNewOrder = mapper.Map<Order>(orderDTOs);
            orderDTOs.Order_Date =DateTimeOffset.Now.LocalDateTime;
            uow.OrderRepository.CreateOrder(CreateNewOrder);
            await uow.SaveChanges();
            return StatusCode(201);
        }
        [HttpGet("order/delivery")]
        public IActionResult GetOrdersBydelivery()
        {
            var orders = uow.OrderRepository.GetOrdersBydelivery();
            return Ok(orders);
        }
        // PUT api/Order/update/5

        [HttpPut("Orders/update")]
        public async Task<IActionResult> UpdateOrders(int id)
        {
            var orderDTOs = new OrderStatusDTOs();
            orderDTOs.Id = id = int.Parse(HttpContext.Request.Form["id"].ToString());

            orderDTOs.OrderStatus = HttpContext.Request.Form["OrderStatus"].ToString();

            //if (id != orderDTOs.Id)
            //    return BadRequest("Update not allowed");

            //if (orderFromDb == null)
            //    return BadRequest("Order not found");
            if (id == orderDTOs.Id)
            {
                var Update = await uow.OrderRepository.EditAsyncTest(id, orderDTOs);
                if (Update != null) return Ok();
            }


            return BadRequest(401);
        }
        // DELETE api/Order/delete/5
        [HttpPut("Orders/Delete/{id}")]
        public async Task<IActionResult> DeleteOrders(int id)
        {
            var OrdersDelete = await uow.OrderRepository.GetOrderById(id);

            //uow.OrderRepository.Delete(id, OrdersDelete);
            await uow.SaveChanges();
            return Ok(id);
        }
    }
}
