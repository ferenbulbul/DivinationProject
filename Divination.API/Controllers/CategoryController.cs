using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Divination.Application.Models.DTOs;
using Divination.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Divination.API.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest();
            }
            else
            {
                await _categoryService.AddCtegory(categoryDto);
                return Ok();
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(CategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest();
            }
            else
            {
                await _categoryService.UpdateCategory(categoryDto);
                return Ok();
            }
        }

        [HttpPut]
        public async Task<IActionResult> IsActiveCategory(int id)
        {
                await _categoryService.IsActiveCategory(id);
                return Ok();   
        }

        [HttpGet]
        public async Task<IActionResult> GetallCategory()
        {
                var categories=await _categoryService.GetallCategoryAsync();
                return Ok(categories);   
        }
    }
}