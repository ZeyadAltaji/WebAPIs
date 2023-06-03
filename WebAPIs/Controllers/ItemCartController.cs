using AutoMapper;
using ECommerce.Application.Abstractions;
using ECommerce.Application.Abstractions.Command;
using ECommerce.Application.DTOs;
using ECommerce.Application.UnitOfWork;
using ECommerce.DataAccessLayer.Repositories;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemCartController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public ItemCartController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCaritem(List<ItemCartDTOs> cartItems)
        {
            var newCartItems = mapper.Map<List<ItemCart>>(cartItems);

            foreach (var item in newCartItems)
            {
                 uow.CartItemRepository.Create(item);
            }
            var createdItems = mapper.Map<List<ItemCartDTOs>>(newCartItems);
             return StatusCode(201);
        }
        [HttpGet("ItemCart")]
        public async Task<IActionResult> GetALlItem()
        {
            var AllItem = await uow.CartItemRepository.GetAll();
            var BrandDTOs = mapper.Map<IEnumerable<ItemCartDTOs>>(AllItem);
            return Ok(BrandDTOs);

        }
        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetByCustomerId(int customerId)
        {
            var itemCarts = await uow.CartItemRepository.GetByCustomerId(customerId);
            return Ok(itemCarts);
        }
        [HttpPut("update-items")]
        public IActionResult UpdateItems([FromBody] IEnumerable<ItemCart> itemCarts)
        {
            if (itemCarts == null)
            {
                return BadRequest();
            }

            uow.CartItemRepository.UpdateItems(itemCarts);

            return Ok();
        }
        [HttpGet("customer/{customerId}/cart/{cartId}")]
        public async Task<ActionResult<IEnumerable<ItemCart>>> GetByCustomerAndCartId(int customerId, int cartId)
        {
            var cartItems =  uow.CartItemRepository.GetByCustomerCartId(customerId, cartId);
            return Ok(cartItems);
        }
        [HttpDelete("{itemCartId}")]
        public IActionResult Delete(int itemCartId)
        {
            uow.CartItemRepository.Deleteitem(itemCartId);
            return Ok();
        }
    }
}
