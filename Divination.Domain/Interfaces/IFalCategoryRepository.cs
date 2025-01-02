using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Divination.Domain.Entities;

namespace Divination.Domain.Interfaces
{
    public interface IFalCategoryRepository : IBaseRepository<FalCategory>
    {
        Task<ICollection<FalCategory>> GetFalCategoriesAsync(List<int> falcategoryId);
    }
}