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
    public class CouponController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public CouponController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;

        }
        // GET: api/Coupon/GetAllCoupon
        
        [HttpGet("GetAllCoupon")]
        public async Task<IActionResult> GetALlCoupon()
        {
            var AllCoupon = await uow.repositoryCoupon.GetAll();
            var CouponDTOs = mapper.Map<IEnumerable<CouponDTOs>>(AllCoupon);
            return Ok(CouponDTOs);

        }
        // GET: api/Coupon/Couponbyid/id
        [HttpGet("Couponbyid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Couponbyid = await uow.repositoryCoupon.GetByID(id);
            return Ok(Couponbyid);
        }

        // POST api/Coupon
        [HttpPost]
        public async Task<IActionResult> CreateBrands(CouponDTOs couponDTOs )
        {
            var CreateNewCoupon = mapper.Map<Coupon>(couponDTOs);
            couponDTOs.CreateDate = DateTime.Now;
            uow.repositoryCoupon.Create(CreateNewCoupon);
            await uow.SaveChanges();
            return StatusCode(201);
        }

        // PUT api/Coupon/update/5
        [HttpPut("Coupons/update/{id}")]
        public async Task<IActionResult> UpdateCoupon(int id, CouponDTOs couponDTOs)
        {
            if (id != couponDTOs.Id)
                return BadRequest("Update not allowed");
            var CouponFromDb = await uow.repositoryCoupon.GetByID(id);

            if (CouponFromDb == null)
                return BadRequest("Update not allowed");
            mapper.Map(couponDTOs, CouponFromDb);

            await uow.SaveChanges();
            return StatusCode(200);
        }

        // DELETE api/Coupon/delete/1
        [HttpPut("Coupons/Delete/{id}")]
        public async Task<IActionResult> DeleteCoupon(int id)
        {
            var CouponDelete = await uow.repositoryCoupon.GetByID(id);

            uow.repositoryCoupon.Delete(id, CouponDelete);
            await uow.SaveChanges();
            return Ok(id);
        }
    }
}
