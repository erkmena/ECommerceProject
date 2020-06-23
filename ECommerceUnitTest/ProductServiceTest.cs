using AutoMapper;
using ECommerce.Business;
using ECommerce.Business.Models.DTO;
using ECommerce.Business.Services;
using ECommerce.EFCore.EFService.Interface;
using ECommerce.EFCore.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.UnitTest
{
    public class ProductServiceTest
    {
        private Product product;
        private IMapper _mapper;
        private Mock<IProductEFService> productEFServiceMock;
        private const int MockProductId = 10;
        [SetUp]
        public void Setup()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile());
            });
            _mapper = mapperConfig.CreateMapper();

            productEFServiceMock = new Mock<IProductEFService>();
            productEFServiceMock.Setup(cef => cef.AddProductAsync(It.IsAny<ProductModel>())).Returns(Task.FromResult(MockProductId));
            
            product = new Product(_mapper, productEFServiceMock.Object);
        }

        [Test]
        public async Task CreateNewProductTest()
        {
            ProductDTO productDTO = _mapper.Map<ProductDTO>(MockModels.MockProductModel());
            int productId = await product.CreateProductAsync(productDTO);
            Assert.AreEqual(productId, MockProductId);
        }
    }
}
