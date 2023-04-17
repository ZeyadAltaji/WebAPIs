using AutoMapper;
using ECommerce.Application.Abstractions;
using ECommerce.Application.DTOs;
using ECommerce.Application.ImageDTOs;
using ECommerce.Application.UnitOfWork;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly IPhotoService photoService;

        public BrandsController(IUnitOfWork uow, IMapper mapper, IPhotoService photoService)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.photoService = photoService;


        }
        // GET: api/Brands/GetAllBrand
        [HttpGet("GetAllBrand")]
        public async Task<IActionResult> GetALlBrands()
        {
            var AllBrand = await uow.repositoryBrands.GetAll();
            var BrandDTOs = mapper.Map<IEnumerable<BrandsDTOs>>(AllBrand);
            return Ok(BrandDTOs);

        }
        // GET: api/Brands/Brand/id
        [HttpGet("Brand/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var BrandsByID = await uow.repositoryBrands.GetByID(id);
            return Ok(BrandsByID);
        }

        // POST api/Brands
        [HttpPost]
        public async Task<IActionResult> CreateBrands(BrandsDTOs brandsDTOs)
        {
            var CreateNewBrands = mapper.Map<Brands>(brandsDTOs);
            brandsDTOs.CreateDate = DateTime.Now;
            uow.repositoryBrands.Create(CreateNewBrands);
            await uow.SaveChanges();
            return StatusCode(201);
        }

        // PUT api/Brand/update/5
        [HttpPut("Brand/update/{id}")]
        public async Task<IActionResult> UpdateBrands(int id, BrandsDTOs brandsDTOs)
        {
            if (id != brandsDTOs.Id)
                return BadRequest("Update not allowed");
            var BrandFromDb = await uow.repositoryBrands.GetByID(id);

            if (BrandFromDb == null)
                return BadRequest("Update not allowed");
            mapper.Map(brandsDTOs, BrandFromDb);

            await uow.SaveChanges();
            return StatusCode(200);
        }

        // DELETE api/<BrandsDeleteController>/5
        [HttpPut("Brand/Delete/{id}")]
        public async Task<IActionResult> DeleteBrands(int id)
        {
            var BrandsDelete = await uow.repositoryBrands.GetByID(id);

            uow.repositoryBrands.Delete(id, BrandsDelete);
            await uow.SaveChanges();
            return Ok(id);
        }
        [HttpPost("Brand/UploadIamge/{BrandID}")]
        public async Task<ActionResult<BrandsImageDtos>>UploadImageBrands(int BrandID,IFormFile BrandFiles)
        {
            var Brand = await uow.repositoryBrands.GetByID(BrandID);
            if (Brand == null)
            {
                return NotFound();
            }
            var BrandRes = await photoService.UploadPhotoAsync(BrandFiles);
            if (BrandRes == null || string.IsNullOrEmpty(BrandRes.PublicId) || BrandRes.SecureUri == null)
            {
                return BadRequest("Invalid upload Result");
            }
            Brand.Image_BrandUrl = BrandRes.SecureUri.AbsoluteUri;
            Brand.Public_id = BrandRes.PublicId;
            uow.repositoryBrands.Update(BrandID, Brand);
            await uow.SaveChanges();
            return Ok(Brand);
        }
    }
}
