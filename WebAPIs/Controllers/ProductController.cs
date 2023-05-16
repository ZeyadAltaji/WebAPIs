using AutoMapper;
using ECommerce.Application.Abstractions;
using ECommerce.Application.DTOs;
using ECommerce.Application.UnitOfWork;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WebAPIs.Services;
using static System.Net.Mime.MediaTypeNames;

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly IPhotoService photoService;

        public ProductController(IUnitOfWork uow, IMapper mapper, IWebHostEnvironment environment, IPhotoService photoService)
        {
            this.photoService=photoService;
            this.uow = uow;
            this.mapper = mapper;
            _environment = environment;
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
            var ProductDTO = mapper.Map<ProductDTOs>(ProductGetByID);
            return Ok(ProductDTO);
        }

        // POST api/Product
        [HttpPost]
        public async Task<IActionResult> CreateProducts([FromForm]ProductDTOs productDTOs)
        {
            var CreateNewProduct = mapper.Map<Product>(productDTOs);
            CreateNewProduct.IsPrimaryImage = await SaveImage(productDTOs.Primary_Image);
            CreateNewProduct.IsForeignImage1 = await SaveImage(productDTOs.ForeignImage1);
            CreateNewProduct.IsForeignImage2 = await SaveImage(productDTOs.ForeignImage2);
            productDTOs.CreateDate= DateTimeOffset.Now.LocalDateTime;
            uow.repositoryProduct.Create(CreateNewProduct);
            await uow.SaveChanges();
            return StatusCode(201);
        }

        // PUT api/Products/update/5
        [HttpPut("Products/update")]
        public async Task<IActionResult> UpdateProducts(int id)
        {
            try
            
            {
                var productDTOs = new ProductDTOs();

                productDTOs.Id = id = int.Parse(HttpContext.Request.Form["id"].ToString());
                var img = HttpContext.Request.Form.Files["Primary_Image"];
                var img1 = HttpContext.Request.Form.Files["ForeignImage1"];
                var img2 = HttpContext.Request.Form.Files["ForeignImage2"];

              
                productDTOs.Title = HttpContext.Request.Form["Title"].ToString();
                 
            
                if (!string.IsNullOrEmpty(HttpContext.Request.Form["Offers"].ToString()))
                {
                    productDTOs.offers = double.Parse(HttpContext.Request.Form["Offers"].ToString());
                }
            

                if (!StringValues.IsNullOrEmpty(HttpContext.Request.Form["Brands_Id"]) && HttpContext.Request.Form["Brands_Id"] != "0")
                {
                    productDTOs.Brands_Id = int.Parse(HttpContext.Request.Form["Brands_Id"]);
                }
                if (!StringValues.IsNullOrEmpty(HttpContext.Request.Form["Car_Id"]) && HttpContext.Request.Form["Car_Id"] != "0")
                {
                    productDTOs.Car_Id = int.Parse(HttpContext.Request.Form["Car_Id"]);
                }
                if (!StringValues.IsNullOrEmpty(HttpContext.Request.Form["Category_Id"]) && HttpContext.Request.Form["Category_Id"] != "0")
                {
                    productDTOs.Category_Id = int.Parse(HttpContext.Request.Form["Category_Id"]);
                }
                
                if (!StringValues.IsNullOrEmpty(HttpContext.Request.Form["Admin_Id"]) && HttpContext.Request.Form["Admin_Id"] != "0")
                {
                    productDTOs.Admin_Id = int.Parse(HttpContext.Request.Form["Admin_Id"]);
                }


                if (id != productDTOs.Id)
                    return BadRequest("Update not allowed");
                var productFromDb = await uow.repositoryProduct.GetByID(id);

                if (productFromDb == null)
                    return BadRequest("Update not allowed");
                if (id == productDTOs.Id)
                {
                    if (img != null && img.Length > 0 || img1 != null && img1.Length > 0 )
                    {
                        // A new image is selected
                        var Update = await uow.ProductRepository.EditAsyncTest(id, productDTOs, img, img1, img2);
                        if (Update != null) return Ok();
                    }
                    else
                    {
                        // Preserve the existing image
                        var Update = await uow.ProductRepository.EditAsyncTest(id, productDTOs, null, null, null);
                        if (Update != null) return Ok();
                    }
                }
                

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
            return BadRequest(401);


        }

        // DELETE api/Product/5
        [HttpPut("Products/Delete/{id}")]
        public async Task<IActionResult> DeleteProducts(int id)
        {
            var ProductsDelete = await uow.repositoryProduct.GetByID(id);

            uow.repositoryProduct.Delete(id, ProductsDelete);
            await uow.SaveChanges();
            return Ok(id);
        }
        [HttpGet("ByBrand/{brandId}")]
        public async Task<ActionResult<List<Product>>> GetByBrand(int brandId)
        {
            Brands brand = await uow.repositoryBrands.GetByID(brandId);
            if (brand == null)
            {
                return NotFound();
            }

            List<Product> products = await uow.RepositoryProducts.GetProductsByBrand(brand);
            return Ok(products);
        }
        [HttpGet("ByCars/{CarId}")]
        public async Task<ActionResult<List<Product>>> GetByCars(int carId)
        {
            Car car= await uow.repositoryCar.GetByID(carId);
            if (car == null)
            {
                return NotFound();
            }

            List<Product> products = await uow.RepositoryProducts.GetProductsByCars(car);
            return Ok(products);
        }
        [HttpGet("ByCateogers/{CategoryId}")]
        public async Task<ActionResult<List<Product>>> GetByCategory(int categoryId)
        {
            Category category = await uow.repositoryCategory.GetByID(categoryId);
            if (category == null)
            {
                return NotFound();
            }

            List<Product> products = await uow.RepositoryProducts.GetProductsByCategory(category);
            return Ok(products);
        }
        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imagename = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', ' ');
            imagename = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
            string imagePath = Path.Combine(@"D:\project fianal\E-commerce\projects\dashboard\src\assets\image\Products", imagename);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imagename;
        }




    }
}
