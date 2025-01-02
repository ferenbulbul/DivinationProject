using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Divination.Application.Models.DTOs;
using Divination.Domain.Entities;

namespace Divination.Application.Services
{
    public interface ICategoryService
    {
        Task AddCtegory(CategoryDto categoryDto );
        Task UpdateCategory(CategoryDto categoryDto);
        Task IsActiveCategory(int id);
        Task<IEnumerable<CategoryDto>> GetallCategoryAsync();
        Task<IEnumerable<CategoryDto>> GetAllFalCategoryAsync();

    }
}