using ECommerce.Business.Models.DTO;
using System.Threading.Tasks;

namespace ECommerce.Business.Services.Interfaces
{
    public interface IProduct
    {
        /// <summary>
        /// Crates and Save the product to DB. Returns saved ProductId
        /// </summary>
        /// <param name="product">The Product Model To Save</param>
        /// <returns></returns>
        Task<int> CreateProductAsync(ProductDTO product);
        /// <summary>
        /// Return The Product Of Given Id.
        /// </summary>
        /// <param name="productId">Id Of Product To Return</param>
        /// <returns></returns>
        Task<ProductDTO> GetProductAsync(int productId);
    }
}
