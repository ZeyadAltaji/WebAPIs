using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.UnitOfWork;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public ProductController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        [HttpGet("AllProduct")]
        public async Task<IActionResult> GetALlProduct()
        {
            var ProductGetAll = await uow.repositoryProduct.GetAll();
            var ProductDtos = mapper.Map<IEnumerable<ProductDTOs>>(ProductGetAll);
            return Ok(ProductDtos);

        }
        // GET: api Products/Product/id
        [HttpGet("Products/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ProductGetByID = await uow.repositoryProduct.GetByID(id);
            return Ok(ProductGetByID);
        }

        // POST api/Product
        [HttpPost]
        public async Task<IActionResult> CreateProducts(ProductDTOs productDTOs)
        {
            var CreateNewProduct = mapper.Map<Product>(productDTOs);
            productDTOs.CreateDate= DateTime.Now;
            uow.repositoryProduct.Create(CreateNewProduct);
            await uow.SaveChanges();
            return StatusCode(201);
        }

        // PUT api/Products/update/5
        [HttpPut("Products/update/{id}")]
        public async Task<IActionResult> UpdateProducts(int id, ProductDTOs productDTOs)
        {
            if (id != productDTOs.Id)
                return BadRequest("Update not allowed");
            var productFromDb = await uow.repositoryProduct.GetByID(id);

            if (productFromDb == null)
                return BadRequest("Update not allowed");
            mapper.Map(productDTOs, productFromDb);

            await uow.SaveChanges();
            return StatusCode(200);
        }

        // DELETE api/Product/5
        [HttpDelete("Products/Delete/{id}")]
        public async Task<IActionResult> DeleteProducts(int id)
        {
            var ProductsDelete = await uow.repositoryProduct.GetByID(id);

            uow.repositoryProduct.Delete(id, ProductsDelete);
            await uow.SaveChanges();
            return Ok(id);
        }
    }
}
