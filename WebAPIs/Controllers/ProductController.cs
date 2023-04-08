using AutoMapper;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using ECommerce.Application.Abstractions;
using ECommerce.Application.DTOs;
using ECommerce.Application.UnitOfWork;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpPost("Products/UploadImage")]
        public async Task<ActionResult> UploadImage()
        {
            bool Results = false;
            try
            {
                var _uploadedfiles = Request.Form.Files;
                foreach (IFormFile source in _uploadedfiles)
                {
                    string Filename = source.FileName;
                    string Filepath = GetFilePath(Filename);

                    if (!System.IO.Directory.Exists(Filepath))
                    {
                        System.IO.Directory.CreateDirectory(Filepath);
                    }

                    string imagepath = Filepath + "\\image.png";

                    if (System.IO.File.Exists(imagepath))
                    {
                        System.IO.File.Delete(imagepath);
                    }
                    using (FileStream stream = System.IO.File.Create(imagepath))
                    {
                        await source.CopyToAsync(stream);
                        Results = true;
                    }


                }
            }
            catch (Exception ex)
            {

            }
            return Ok(Results);
        }
        [NonAction]
        private string GetFilePath(string ProductCode)
        {
            return this._environment.WebRootPath + "\\Uploads\\Product\\" + ProductCode;
        }

        [HttpPost("Products/UploadImages/{propId}")]
        public async Task<IActionResult> AddPropertyPhoto(IFormFile Products_files, int propId)
        {
            var res = await photoService.UploadPhotoAsync(Products_files);
            if (res.Error != null)
            {
                return BadRequest(res.Error.Message);

            }
            var property = await uow.repositoryProduct.GetByID(propId);
            var photo = new Photo
            {
                main_ImageUrl = res.SecureUrl.AbsoluteUri,
                sub_Image1Url = res.SecureUrl.AbsoluteUri,
                sub_Image2Url = res.SecureUrl.AbsoluteUri,
                publicID = res.PublicId
            };
            if (property.Image.Count == 0)
            {
                photo.main_Image = true;
                photo.sub_Image1 = false;
                photo.sub_Image1 = false;

            }
            property.Image.Add(photo);
            await uow.SaveChanges();

            return StatusCode(201);
        }
        //public async Task<IActionResult> AddPropertyPhoto(List<IFormFile> Products_files, int propId)
        //{
        //    var results = await photoService.UploadPhotosAsync(Products_files);
        //    if (results.Any(r => r.Error != null))
        //        return BadRequest("Error uploading one or more photos.");

        //    var product = await uow.repositoryProduct.GetByID(propId);
        //    foreach (var result in results)
        //    {
        //        var photo = new Photo
        //        {
        //            main_ImageUrl = result.SecureUri.AbsoluteUri,
        //            sub_Image1Url = result.SecureUri.AbsoluteUri,
        //            sub_Image2Url = result.SecureUri.AbsoluteUri,
        //            publicID = result.PublicId,
        //        };
        //        if(product.Image.Count == 0)
        //        {
        //            photo.main_Image = true;
        //            photo.sub_Image1 = false;
        //            photo.sub_Image2 = false;
        //        }
        //        product.Image.Add(photo);
        //    }
        //    await uow.SaveChanges();
        //    return Ok(201);
        //}

    }
}
