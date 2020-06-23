using AutoMapper;
using ECommerce.Business.Models.DTO;
using ECommerce.Business.Services.Interfaces;
using ECommerce.EFCore.EFService.Interface;
using ECommerce.EFCore.Models;
using System.Threading.Tasks;

namespace ECommerce.Business.Services
{
    public class Product : IProduct
    {
        IMapper _mapper;
        IProductEFService _productEFService;

        public Product(IMapper mapper, IProductEFService productEFService)
        {
            _mapper = mapper;
            _productEFService = productEFService;
        }
        public async Task<int> CreateProductAsync(ProductDTO product)
        {
            ProductModel productModel = _mapper.Map<ProductModel>(product);
            return await _productEFService.AddProductAsync(productModel);
        }
        public async Task<ProductDTO> GetProductAsync(int productId)
        {
            return _mapper.Map<ProductDTO>(await _productEFService.GetProductAsync(productId));
        }
    }
}
