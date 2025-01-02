using System;
using System.Collections.Generic;
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
                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = "Category data is null.",
                    Data = null
                };
                return BadRequest(response);
            }

            await _categoryService.AddCtegory(categoryDto);

            var successResponse = new ApiResponse<string>
            {
                Success = true,
                Message = "Category added successfully.",
                Data = null
            };
            return Ok(successResponse);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(CategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = "Category data is null.",
                    Data = null
                };
                return BadRequest(response);
            }

            await _categoryService.UpdateCategory(categoryDto);

            var successResponse = new ApiResponse<string>
            {
                Success = true,
                Message = "Category updated successfully.",
                Data = null
            };
            return Ok(successResponse);
        }

        [HttpPut]
        public async Task<IActionResult> IsActiveCategory(int id)
        {
            if (id <= 0)
            {
                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = "Invalid category ID.",
                    Data = null
                };
                return BadRequest(response);
            }

            await _categoryService.IsActiveCategory(id);

            var successResponse = new ApiResponse<string>
            {
                Success = true,
                Message = "Category status updated successfully.",
                Data = null
            };
            return Ok(successResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetallCategory()
        {
            var categories = await _categoryService.GetallCategoryAsync();

            var response = new ApiResponse<IEnumerable<CategoryDto>>
            {
                Success = true,
                Message = "Categories retrieved successfully.",
                Data = categories
            };
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetallFalCategory()
        {
            var categories = await _categoryService.GetAllFalCategoryAsync();

            var response = new ApiResponse<IEnumerable<CategoryDto>>
            {
                Success = true,
                Message = "Categories retrieved successfully.",
                Data = categories
            };
            return Ok(response);
        }
    }
}
