using ECommerce.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.EFCore.EFService.Interface
{
    public interface IProductEFService
    {
        Task<int> AddProductAsync(ProductModel productModel);
        Task<ProductModel> GetProductAsync(int productId);
    }
}
