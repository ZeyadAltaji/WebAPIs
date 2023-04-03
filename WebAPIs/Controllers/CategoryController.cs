﻿using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.UnitOfWork;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public CategoryController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;

        }
        // GET: api/Category/categorise
        [HttpGet("categorise")]
       public async Task<IActionResult> GetALlCategory()
        {
            var categorise = await uow.repositoryCategory.GetAll();
            var categoriseDTOs= mapper.Map<IEnumerable<CategoryDTOs>>(categorise);
            return Ok(categoriseDTOs);

        }
        // GET: api/Category/categorise/id
        [HttpGet("categorise/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var categoriseByID = await uow.repositoryCategory.GetByID(id);
            return Ok(categoriseByID);
        }

        // POST api/Category/categorise
        [HttpPost]
        public async Task<IActionResult> Createcategorise(CategoryDTOs categoryDTOs)
        {
            var CreateNewcategorise = mapper.Map<Category>(categoryDTOs);
            categoryDTOs.CreateDate = DateTime.Now;
            uow.repositoryCategory.Create(CreateNewcategorise);
            await uow.SaveChanges();
            return StatusCode(201);
        }

        // PUT api/categorise/update/5
        [HttpPut("categorise/update/{id}")]
        public async Task<IActionResult> Updatecategorise(int id, CategoryDTOs categoryDTOs)
        {
            if (id != categoryDTOs.Id)
                return BadRequest("Update not allowed");
            var categoriseFromDb = await uow.repositoryCategory.GetByID(id);

            if (categoriseFromDb == null)
                return BadRequest("Update not allowed");
            mapper.Map(categoryDTOs, categoriseFromDb);

            await uow.SaveChanges();
            return StatusCode(200);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("categorise/Delete/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var categoriseDelete = await uow.repositoryCategory.GetByID(id);

            uow.repositoryCategory.Delete(id,categoriseDelete);
            await uow.SaveChanges();
            return Ok(id);
        }
    }
}
