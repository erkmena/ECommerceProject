using AutoMapper;
using ECommerce.Business.Services;
using ECommerce.EFCore.EFService.Interface;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ECommerce.Business.Models.DTO;
using System.Collections.Generic;
using ECommerce.Business.Models;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Ecommerce.WebApplication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private IMapper _mapper;
        private ICategoryEFService _categoryEFService;
        private IProductEFService _productEFService;
        private ICartEFService _cartEFService;
        private ICampaignEFService _campaignEFService;
        private ICouponEFService _couponEFService;
        private IDeliveryEFService _deliveryEFService;
        private Microsoft.Extensions.Configuration.IConfiguration _configuration;
        public IndexModel(ILogger<IndexModel> logger, IMapper mapper, ICategoryEFService categoryEFService, IProductEFService productEFService, ICartEFService cartEFService,
                                ICampaignEFService campaignEFService, ICouponEFService couponEFService, IDeliveryEFService deliveryEFService,
                                Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _categoryEFService = categoryEFService;
            _productEFService = productEFService;
            _cartEFService = cartEFService;
            _campaignEFService = campaignEFService;
            _couponEFService = couponEFService;
            _deliveryEFService = deliveryEFService;
            _configuration = configuration;
        }

        public async Task OnGet()
        {
            CategoryDTO categoryDTO = new CategoryDTO()
            {
                Tittle = "Food2"
            };
            Category category = new Category(_mapper, _categoryEFService);
            categoryDTO.CategoryId = await category.CreateNewCategoryAsync(categoryDTO);

            ProductDTO productAppleDTO = new ProductDTO()
            {
                CategoryId = categoryDTO.CategoryId,
                Price = 100,
                Tittle = "Apple"
            };

            ProductDTO productAlmondDTO = new ProductDTO()
            {
                CategoryId = categoryDTO.CategoryId,
                Price = 150,
                Tittle = "Almond"
            };

            Product product = new Product(_mapper, _productEFService);
            productAppleDTO.ProductId = await product.CreateProductAsync(productAppleDTO);
            productAlmondDTO.ProductId = await product.CreateProductAsync(productAlmondDTO);

            Cart cart = new Cart(_cartEFService, _mapper,_productEFService,_categoryEFService);

            CartDetailDTO cartDetailDTO1 = new CartDetailDTO()
            {
                ProductId = productAlmondDTO.ProductId,
                ProductQuantity = 3
            };

            CartDetailDTO cartDetailDTO2 = new CartDetailDTO()
            {
                ProductId = productAppleDTO.ProductId,
                ProductQuantity = 1
            };

            CartDTO cartDTO = new CartDTO()
            {
                CustomerId = 1
            };
            cartDTO.CartId = await cart.CreateCartAsync(cartDTO);

            await cart.AddItemAsync(cartDTO.CartId, productAppleDTO, 3);
            List<CampaignDTO> campaigns = new List<CampaignDTO>();

            CampaignDTO campaignDTO1 = new CampaignDTO()
            {
                CategoryId = categoryDTO.CategoryId,
                Discount = 20,
                DiscountType = DiscountType.Rate,
                Quantity = 3
            };
            CampaignDTO campaignDTO2 = new CampaignDTO()
            {
                CategoryId = categoryDTO.CategoryId,
                Discount = 50,
                DiscountType = DiscountType.Rate,
                Quantity = 5
            };
            CampaignDTO campaignDTO3 = new CampaignDTO()
            {
                CategoryId = categoryDTO.CategoryId,
                Discount = 5,
                DiscountType = DiscountType.Amount,
                Quantity = 5
            };

            Campaign campaign = new Campaign(_mapper, _campaignEFService);
            campaignDTO1.CampaignId =await campaign.CreateCampaignAsync(campaignDTO1);
            campaignDTO2.CampaignId =await campaign.CreateCampaignAsync(campaignDTO2);
            campaignDTO3.CampaignId =await campaign.CreateCampaignAsync(campaignDTO3);

            campaigns.Add(campaignDTO1);
            campaigns.Add(campaignDTO2);
            campaigns.Add(campaignDTO3);

            await cart.ApplyCampaignDiscountAsync(cartDTO, campaigns);

            CouponDTO couponDTO = new CouponDTO()
            {
                Discount = 10,
                DiscountType = ECommerce.Business.Models.DiscountType.Rate,
                MinimumCartCost = 100
            };

            Coupon coupon = new Coupon(_mapper, _couponEFService);
            couponDTO.CouponId = await coupon.CreateNewCouponAsync(couponDTO);

            await cart.ApplyCouponDiscountAsync(cartDTO, couponDTO);

            Delivery delivery = new Delivery(_deliveryEFService, _cartEFService, _productEFService, _categoryEFService, _mapper);
            double costPerDelivery = _configuration.GetValue<double>("CostPerDelivery");
            double costPerProduct = _configuration.GetValue<double>("CostPerProduct");
            double fixedCost = _configuration.GetValue<double>("FixedCost");
            double deliveryCost = await delivery.CalculateDeliveryCostAndCountAsync(cartDTO.CartId, costPerDelivery, costPerProduct, fixedCost);

            double cartAmount =await cart.GetTotalAmountAfterDiscountsAsync(cartDTO);
            double couponDiscount = await cart.GetCouponDiscountAsync(cartDTO);
            double campaignDiscount = await cart.GetCampaignDiscountAsync(cartDTO);
            double getDeliveryCost = await delivery.GetDeliveryCostAsync(cartDTO);

            IList<PrintModel> printModels = await cart.PrintAsync(cartDTO);
        }
    }
}
