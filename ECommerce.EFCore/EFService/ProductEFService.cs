using ECommerce.EFCore.Data;
using ECommerce.EFCore.EFService.Interface;
using ECommerce.EFCore.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.EFCore.EFService
{
    public class ProductEFService : IProductEFService
    {
        private readonly IDbContext _dbContext;
        public ProductEFService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> AddProductAsync(ProductModel productModel)
        {
            _dbContext.Products.Add(productModel);
            await _dbContext.SaveChangesAsync();
            return productModel.ProductId;
        }
        public async Task<ProductModel> GetProductAsync(int productId)
        {
            return await _dbContext.Products.Where(p => p.ProductId == productId).FirstOrDefaultAsync();
        }
    }
}
