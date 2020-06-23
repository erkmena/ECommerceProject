using AutoMapper;
using ECommerce.Business;
using ECommerce.Business.Models.DTO;
using ECommerce.Business.Services;
using ECommerce.EFCore.EFService.Interface;
using ECommerce.EFCore.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace ECommerce.UnitTest
{
    public class DeliveryServiceTest
    {
        private Delivery delivery;
        private IMapper _mapper;
        private Mock<IDeliveryEFService> deliveryEFServiceMock;
        private Mock<ICartEFService> cartEFServiceMock;
        private Mock<IProductEFService> productEFServiceMock;
        private Mock<ICategoryEFService> categoryEFServiceMock;
        private const int DeliveryModelId = 1;
        [SetUp]
        public void Setup()
        {
            deliveryEFServiceMock = new Mock<IDeliveryEFService>();
            deliveryEFServiceMock.Setup(d => d.AddDeliveryAsync(It.IsAny<DeliveryModel>())).Returns(Task.FromResult(DeliveryModelId));
            deliveryEFServiceMock.Setup(d => d.GetDeliveriesAsync(It.IsAny<int>())).Returns(Task.FromResult(MockModels.MockCartDeliveries()));

            cartEFServiceMock = new Mock<ICartEFService>();
            cartEFServiceMock.Setup(cef => cef.GetCartAsync(It.IsAny<int>())).Returns(Task.FromResult(MockModels.MockCartModel()));

            productEFServiceMock = new Mock<IProductEFService>();
            productEFServiceMock.Setup(p => p.GetProductAsync(It.IsAny<int>())).Returns(Task.FromResult(MockModels.MockProductModel()));

            categoryEFServiceMock = new Mock<ICategoryEFService>();
            categoryEFServiceMock.Setup(c => c.GetCategoryAsync(It.IsAny<int>())).Returns(Task.FromResult(MockModels.MockCategoryModel()));

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile());
            });
            _mapper = mapperConfig.CreateMapper();

            delivery = new Delivery(deliveryEFServiceMock.Object, cartEFServiceMock.Object, productEFServiceMock.Object,categoryEFServiceMock.Object, _mapper);
        }
        [Test]
        public async Task CalculateDeliveryCostAndCountTest()
        {
            double deliveryCost = await delivery.CalculateDeliveryCostAndCountAsync(1, 5, 4, 2.99);
            Assert.IsTrue(deliveryCost > 0);
        }
        [Test]
        public async Task CalculateForTest()
        {
            double deliveryCost = await delivery.GetDeliveryCostAsync(_mapper.Map<CartDTO>(MockModels.MockCartModel()));
            Assert.IsTrue(deliveryCost > 0);
        }

    }
}
