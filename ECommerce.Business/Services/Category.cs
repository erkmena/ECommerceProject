using AutoMapper;
using ECommerce.Business.Models.DTO;
using ECommerce.Business.Services.Interfaces;
using ECommerce.EFCore.EFService.Interface;
using ECommerce.EFCore.Models;
using System.Threading.Tasks;

namespace ECommerce.Business.Services
{
    public class Category : ICategory
    {
        private IMapper _mapper;
        private ICategoryEFService _categoryEFService;

        public Category(IMapper mapper, ICategoryEFService categoryEFService)
        {
            _mapper = mapper;
            _categoryEFService = categoryEFService;
        }
        public async Task<int> CreateNewCategoryAsync(CategoryDTO category)
        {
            CategoryModel categoryModel = _mapper.Map<CategoryModel>(category);
            return await _categoryEFService.AddCategoryAsync(categoryModel);
        }
        public async Task<CategoryDTO> GetCategoryAsync(int categoryId)
        {
            return _mapper.Map<CategoryDTO>(await _categoryEFService.GetCategoryAsync(categoryId));
        }
    }
}
