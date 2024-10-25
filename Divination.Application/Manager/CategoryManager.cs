using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Divination.Application.Models.DTOs;
using Divination.Application.Services;
using Divination.Domain.Entities;
using Divination.Domain.Interfaces;

namespace Divination.Application.Manager
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryRepository _categoryService;
        public CategoryManager(ICategoryRepository categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task AddCtegory(CategoryDto categoryDto)
        {
            try
            {
                var category = new Category
                {
                    Id = categoryDto.Id,
                    CategoryName = categoryDto.CategoryName,
                };
                await _categoryService.AddAsync(category);
            }
            catch (System.Exception)
            {

                throw;
            }


        }

        public async Task UpdateCategory(CategoryDto categoryDto)
        {
            try
            {
                var category = new Category
                {
                    Id = categoryDto.Id,
                    CategoryName = categoryDto.CategoryName,
                };
                await _categoryService.UpdateAsync(category);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task IsActiveCategory(int id)
        {
            try
            {
                await _categoryService.IsActiveAsync(id);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<CategoryDto>> GetallCategoryAsync()
        {
            try
            {
                var categories = await _categoryService.GetAllAsync();
                var categoryDtos = categories.Select(category => new CategoryDto
                {
                    Id = category.Id,
                    CategoryName = category.CategoryName,
                }).ToList();
                return categoryDtos;
            }
            catch (System.Exception)
            {
                throw;
            }


        }
    }
}