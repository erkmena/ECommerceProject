using ECommerce.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.EFCore.EFService.Interface
{
    public interface ICategoryEFService
    {
        Task<int> AddCategoryAsync(CategoryModel categoryModel);
        Task<CategoryModel> GetCategoryAsync(int categoryId);
    }
}
