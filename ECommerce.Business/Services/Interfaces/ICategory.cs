using ECommerce.Business.Models.DTO;
using System.Threading.Tasks;

namespace ECommerce.Business.Services.Interfaces
{
    public interface ICategory
    {
        /// <summary>
        /// Create and Save New Category To DB and Return CategoryId
        /// </summary>
        /// <param name="category">The Category Model</param>
        /// <returns></returns>
        Task<int> CreateNewCategoryAsync(CategoryDTO category);
        /// <summary>
        /// Returns Category of given CategoryId
        /// </summary>
        /// <param name="categoryId">Category Id To Get</param>
        /// <returns></returns>
        Task<CategoryDTO> GetCategoryAsync(int categoryId);
    }
}
