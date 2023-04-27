using AutoMapper;
using ECommerce.Application.Abstractions;
using ECommerce.Application.DTOs;
using ECommerce.Application.ImageDTOs;
using ECommerce.Application.UnitOfWork;
using ECommerce.DataAccessLayer;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;

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
        private readonly IWebHostEnvironment webHostEnvironment;

        private readonly DBContext _context;


        public BrandsController(IUnitOfWork uow, IMapper mapper, IPhotoService photoService, IWebHostEnvironment webHostEnvironment, DBContext context)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.photoService = photoService;
            this.webHostEnvironment = webHostEnvironment;
            _context = context;
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
        [HttpPost]
        public async Task<IActionResult> CreateBrands([FromForm] BrandsDTOs brandsDTOs)
        {
            var newBrand = mapper.Map<Brands>(brandsDTOs);
            newBrand.Public_id = await SaveImage(brandsDTOs.Image_BrandUrl);
            newBrand.CreateDate = DateTimeOffset.Now.LocalDateTime;
            uow.repositoryBrands.Create(newBrand);
            await uow.SaveChanges();
            return StatusCode(201);
        }
        
        // PUT api/Brand/update
        [HttpPut("update")]
        public async Task<IActionResult> UpdateBrand()
        {
            try
            {
                int id = int.Parse(HttpContext.Request.Form["id"].ToString());
                var img = HttpContext.Request.Form.Files["Image_BrandUrl"];
                var brandDTO = new BrandsDTOs();
                brandDTO.Name = HttpContext.Request.Form["Name"].ToString();

                if (!string.IsNullOrEmpty(brandDTO.Name))
                {
                    if (img != null && img.Length > 0)
                    {
                        // A new image is selected
                        var Update = await uow.repositoryBrand.EditAsyncTest(id, brandDTO, img);
                        if (Update != null) return Ok();
                    }
                    else
                    {
                        // Preserve the existing image
                        var Update = await uow.repositoryBrand.EditAsyncTest(id, brandDTO, null);
                        if (Update != null) return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest();
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

       
        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imagename = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', ' ');
            imagename = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
            string imagePath = Path.Combine(@"D:\project fianal\E-commerce\projects\dashboard\src\assets\image\Brands", imagename);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imagename;
        }
    }
}
