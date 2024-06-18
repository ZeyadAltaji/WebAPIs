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
    public class CartController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public CartController (IUnitOfWork uow , IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCart (CartDTOs cartDTOs)
        {
            var CreateNewproduct = mapper.Map<Cart>(cartDTOs);
            CreateNewproduct.Customer_Id = cartDTOs.Customer_Id; // Set the Customer_Id property

            CreateNewproduct.CreatedAt = DateTimeOffset.Now.LocalDateTime;
            uow.cartRepository.Create(CreateNewproduct);
            var createdCart = await uow.cartRepository.GetById(CreateNewproduct.Id);

            return StatusCode(201 , createdCart);
        }
    }
}
