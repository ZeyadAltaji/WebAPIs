using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.UnitOfWork;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubProductController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public SubProductController(IUnitOfWork uow, IMapper mapper)
        {
                this.uow=uow;
            this.mapper=mapper;
        }
        [HttpGet("GetAllproducts")]
        public async Task<IActionResult> GetallProducts()
        {
            var allproduct = await uow.repositorySubProducts.GetAll();
            var getbyDTOs = mapper.Map<IEnumerable<SubProducts>>(allproduct);
            return Ok(getbyDTOs);
        }
        [HttpGet("Products/{id}")]
        public async Task<IActionResult>GetByID(int id)
        {
            var productByID = await uow.repositorySubProducts.GetByID(id);
            return Ok(productByID);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCar([FromForm] SubProductDTOs productDTOs)
        {
            var CreateNewproduct = mapper.Map<SubProducts>(productDTOs);
            CreateNewproduct.IsPrimaryImage = await SaveImage(productDTOs.Primary_Image);

            productDTOs.CreateDate = DateTimeOffset.Now.LocalDateTime;
            uow.repositorySubProducts.Create(CreateNewproduct);
            await uow.SaveChanges();
            return StatusCode(201);
        }
        [HttpGet("ByProducts/{ProductsId}")]
        public async Task<ActionResult<List<Product>>> GetByProducts(int ProductsId)
        {          
            var product = await uow.repositoryProduct.GetByID(ProductsId);

            if (product == null)
            {
                return NotFound();
            }

            // Call GetSubProductsByProducts method to retrieve related SubProducts
            var subProducts = await uow.RepositorySubProducts.GetSubProductsByProducts(product);
            return Ok(subProducts);

        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateCar(int id)
        {
            try
            {
                var productDTOs = new SubProductDTOs();


                productDTOs.Id = id = int.Parse(HttpContext.Request.Form["id"].ToString());
                var img = HttpContext.Request.Form.Files["Primary_Image"];
                productDTOs.Serial_Id = HttpContext.Request.Form["Serial_Id"].ToString();
                productDTOs.Title = HttpContext.Request.Form["Title"].ToString();
                productDTOs.Description = HttpContext.Request.Form["Description"].ToString();
                if (!string.IsNullOrEmpty(HttpContext.Request.Form["Price"].ToString()))
                {
                    productDTOs.Price = double.Parse(HttpContext.Request.Form["Price"].ToString());
                }
                if (!string.IsNullOrEmpty(HttpContext.Request.Form["Offers"].ToString()))
                {
                    productDTOs.offers = double.Parse(HttpContext.Request.Form["Offers"].ToString());
                }
                if (!string.IsNullOrEmpty(HttpContext.Request.Form["New_price"].ToString()))
                {
                    productDTOs.New_price = double.Parse(HttpContext.Request.Form["New_price"].ToString());
                }
                if (!StringValues.IsNullOrEmpty(HttpContext.Request.Form["Quantity"]) && HttpContext.Request.Form["Quantity"] != "0")
                {
                    productDTOs.Quantity = int.Parse(HttpContext.Request.Form["Quantity"]);
                }

                if (!StringValues.IsNullOrEmpty(HttpContext.Request.Form["Brands_Id"]) && HttpContext.Request.Form["Brands_Id"] != "0")
                {
                    productDTOs.Brands_Id = int.Parse(HttpContext.Request.Form["Brands_Id"]);
                }
                if (!StringValues.IsNullOrEmpty(HttpContext.Request.Form["Car_Id"]) && HttpContext.Request.Form["Car_Id"] != "0")
                {
                    productDTOs.Car_Id = int.Parse(HttpContext.Request.Form["Car_Id"]);
                }
                if(!StringValues.IsNullOrEmpty(HttpContext.Request.Form["productId"]) && HttpContext.Request.Form["productId"] != "0")
                {
                    productDTOs.productId = int.Parse(HttpContext.Request.Form["productId"]);
                }
                if (!StringValues.IsNullOrEmpty(HttpContext.Request.Form["Category_Id"]) && HttpContext.Request.Form["Category_Id"] != "0")
                {
                    productDTOs.Category_Id = int.Parse(HttpContext.Request.Form["Category_Id"]);
                }
                if (!StringValues.IsNullOrEmpty(HttpContext.Request.Form["Customer_Id"]) && HttpContext.Request.Form["Customer_Id"] != "0")
                {
                    productDTOs.Customer_Id = int.Parse(HttpContext.Request.Form["Customer_Id"]);
                }
                if (!StringValues.IsNullOrEmpty(HttpContext.Request.Form["Admin_Id"]) && HttpContext.Request.Form["Admin_Id"] != "0")
                {
                    productDTOs.Admin_Id = int.Parse(HttpContext.Request.Form["Admin_Id"]);
                }
                if (id != productDTOs.Id)
                    return BadRequest("Update not allowed");

                var productFromDb = await uow.repositorySubProducts.GetByID(id);

                if (productFromDb == null)
                    return BadRequest("Update not allowed");

                if (id == productDTOs.Id)
                {
                    if (img != null && img.Length > 0)
                    {
                        // A new image is selected
                        var Update = await uow.repositoryCars.EditAsyncTest(id, productDTOs, img);
                        if (Update != null) return Ok();
                    }
                    else
                    {
                        // Preserve the existing image
                        var Update = await uow.repositoryCars.EditAsyncTest(id, productDTOs, null);
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
        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var productDelete = await uow.repositorySubProducts.GetByID(id);

            uow.repositorySubProducts.Delete(id, productDelete);
            await uow.SaveChanges();
            return Ok(id);
        }
        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imagename = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', ' ');
            imagename = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
            string imagePath = Path.Combine(@"D:\project fianal\E-commerce\projects\dashboard\src\assets\image\SubProduct", imagename);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imagename;
        }
    }
}
