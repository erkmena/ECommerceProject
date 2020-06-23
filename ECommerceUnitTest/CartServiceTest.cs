using AutoMapper;
using ECommerce.Business;
using ECommerce.Business.Models;
using ECommerce.Business.Models.DTO;
using ECommerce.Business.Services;
using ECommerce.EFCore.EFService.Interface;
using ECommerce.EFCore.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.UnitTest
{
    public class CartServiceTest
    {
        private Cart cart;
        private Mock<ICartEFService> cartEFServiceMock;
        private Mock<IProductEFService> productEFServiceMock;
        private Mock<ICategoryEFService> categoryEFServiceMock;
        private IMapper _mapper;
        private CartDTO _cartDTO;
        private const int CartId = 1;
        private const int CartDetailId = 2;
        [SetUp]
        public void Setup()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile());
            });
            _mapper = mapperConfig.CreateMapper();

            cartEFServiceMock = new Mock<ICartEFService>();
            productEFServiceMock = new Mock<IProductEFService>();
            categoryEFServiceMock = new Mock<ICategoryEFService>();

            //Mock Setups
            cartEFServiceMock.Setup(c => c.GetCartAsync(It.IsAny<int>())).Returns(Task.FromResult(MockModels.MockCartModel()));
            cartEFServiceMock.Setup(c => c.AddCartAsync(It.IsAny<CartModel>())).Returns(Task.FromResult(CartId));
            cartEFServiceMock.Setup(c => c.AddCartDetailAsync(It.IsAny<CartDetailModel>())).Returns(Task.FromResult(CartDetailId));
            cartEFServiceMock.Setup(c => c.SaveCartCampaignAsync(It.IsAny<CartCampaignModel>(), It.IsAny<int>()));
            cartEFServiceMock.Setup(c => c.SaveCartCouponAsync(It.IsAny<CartCouponModel>(), It.IsAny<int>()));
            cartEFServiceMock.Setup(c => c.GetCartCampaignAsync(It.IsAny<int>())).Returns(Task.FromResult(MockModels.MockListCartCampaignModel()));
            cartEFServiceMock.Setup(c => c.GetCartCouponAsync(It.IsAny<int>())).Returns(Task.FromResult(MockModels.MockCartCouponModel()));
            cartEFServiceMock.Setup(c => c.GetCartDeliveriesAsync(It.IsAny<int>())).Returns(Task.FromResult(MockModels.MockCartDeliveries()));
            cartEFServiceMock.Setup(c => c.GetCartDetailsAsync(It.IsAny<int>())).Returns(Task.FromResult(MockModels.MockListCartDetailModel()));

            productEFServiceMock.Setup(p => p.GetProductAsync(It.IsAny<int>())).Returns(Task.FromResult(MockModels.MockProductModel()));

            categoryEFServiceMock.Setup(c => c.GetCategoryAsync(It.IsAny<int>())).Returns(Task.FromResult(MockModels.MockCategoryModel()));

            _cartDTO = _mapper.Map<CartDTO>(MockModels.MockCartModel());

            cart = new Cart(cartEFServiceMock.Object, _mapper, productEFServiceMock.Object, categoryEFServiceMock.Object);
        }

        [Test]
        public async Task AddItemTest()
        {
            ProductDTO productDTO = _mapper.Map<ProductDTO>(MockModels.MockProductModel());
            int cartItemId = await cart.AddItemAsync(_cartDTO.CartId, productDTO, 4);
            Assert.IsTrue(cartItemId > 0);
        }
        [Test]
        public async Task ApplyCampaignDiscountTest()
        {
            List<CampaignDTO> campaignList = new List<CampaignDTO>() { _mapper.Map<CampaignDTO>(MockModels.MockCampaignModel()), _mapper.Map<CampaignDTO>(MockModels.MockCampaignModel()) };
            double discountAmount = await cart.ApplyCampaignDiscountAsync(_cartDTO, campaignList);
            Assert.Positive(discountAmount);
        }
        [Test]
        public async Task ApplyCouponDiscountTest()
        {
            CouponDTO couponDTO = _mapper.Map<CouponDTO>(MockModels.MockCouponModel());
            double discountAmount = await cart.ApplyCouponDiscountAsync(_cartDTO, couponDTO);
            Assert.Positive(discountAmount);
        }
        [Test]
        public async Task CreateCartTest()
        {
            int cartId = await cart.CreateCartAsync(_mapper.Map<CartDTO>(MockModels.MockCartModel()));
            Assert.AreEqual(cartId, CartId);
        }
        [Test]
        public async Task GetCampaignDiscount()
        {
            double discountAmount = await cart.GetCampaignDiscountAsync(_cartDTO);
            Assert.IsTrue(discountAmount > 0);
        }
        [Test]
        public async Task GetCouponDiscountTest()
        {
            double discountAmount = await cart.GetCouponDiscountAsync(_cartDTO);
            Assert.IsTrue(discountAmount > 0);
        }
        [Test]
        public async Task GetDeliveryCostTest()
        {
            double discountAmount = await cart.GetDeliveryCostAsync(_cartDTO);
            Assert.IsTrue(discountAmount > 0);
        }
        [Test]
        public async Task GetTotalAmountAfterDiscountsTest()
        {
            double totalAmount = await cart.GetTotalAmountAfterDiscountsAsync(_cartDTO);
            Assert.IsTrue(totalAmount > 0);
        }
        [Test]
        public async Task PrintTest()
        {
            IList<PrintModel> printModel = await cart.PrintAsync(_cartDTO);
            Assert.IsTrue(printModel.Count > 0);
        }
        [Test]
        public async Task GetCartTest()
        {
            CartDTO cartDTO = await cart.GetCartAsync(1);
            Assert.IsTrue(cartDTO != null && cartDTO != new CartDTO());
        }
    }
}