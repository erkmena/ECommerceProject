using ECommerce.EFCore.Data;
using ECommerce.EFCore.EFService.Interface;
using ECommerce.EFCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.EFCore.EFService
{
    public class CategoryEFService : ICategoryEFService
    {
        private readonly IDbContext _dbContext;
        public CategoryEFService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> AddCategoryAsync(CategoryModel categoryModel)
        {
            _dbContext.Categories.Add(categoryModel);
            await _dbContext.SaveChangesAsync();
            return categoryModel.CategoryId;
        }
        public async Task<CategoryModel> GetCategoryAsync(int categoryId)
        {
            return await _dbContext.Categories.Where(c => c.CategoryId == categoryId).FirstOrDefaultAsync();
        }
    }
}
