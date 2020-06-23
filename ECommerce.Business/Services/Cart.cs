using AutoMapper;
using ECommerce.Business.Models;
using ECommerce.Business.Models.DTO;
using ECommerce.Business.Services.Interfaces;
using ECommerce.EFCore.Models;
using System.Collections.Generic;
using System.Linq;
using ECommerce.EFCore.EFService.Interface;
using System.Threading.Tasks;

namespace ECommerce.Business.Services
{
    public class Cart : ICart
    {
        private ICartEFService _cartEFService;
        private IMapper _mapper;
        private IProductEFService _productEFService;
        private ICategoryEFService _categoryEFService;
        public Cart(ICartEFService cartEFService, IMapper mapper, IProductEFService productEFService, ICategoryEFService categoryEFService)
        {
            _cartEFService = cartEFService;
            _mapper = mapper;
            _productEFService = productEFService;
            _categoryEFService = categoryEFService;
        }
        #region Interface Methods

        public async Task<int> AddItemAsync(int cartId, ProductDTO product, int quantity)
        {
            CartDetailDTO cartDetailDTO = new CartDetailDTO()
            {
                CartId = cartId,
                ProductId = product.ProductId,
                ProductQuantity = quantity
            };
            CartDetailModel cartDetailModel = _mapper.Map<CartDetailModel>(cartDetailDTO);
            return await _cartEFService.AddCartDetailAsync(cartDetailModel);
        }
        public async Task<double> ApplyCampaignDiscountAsync(CartDTO cart, IList<CampaignDTO> campaignList)
        {
            double discountAmount = 0;
            CampaignDTO selectedCampaign = null;
            foreach (CampaignDTO campaignItem in campaignList)
            {
                double tempDiscountAmount = await CheckCampaignDiscountAmountAsync(cart, campaignItem); //Apply the ruleset of campaign and return discount amount
                if (tempDiscountAmount > discountAmount) //Get the campaign with highest discount amount
                {
                    discountAmount = tempDiscountAmount;
                    selectedCampaign = campaignItem;
                }
            }
            if (selectedCampaign != null) // If any campaign is selected save and apply the campaign
            {
                await SaveCampaignAsync(cart, selectedCampaign, discountAmount);
            }
            return discountAmount;
        }

        public async Task<double> ApplyCouponDiscountAsync(CartDTO cart, CouponDTO coupon)
        {
            double discountAmount = 0;
            double tempDiscountAmount = await CheckCouponDiscountAmountAsync(cart, coupon); //Apply the ruleset of coupon and return discount amount
            if (tempDiscountAmount > discountAmount)//if discount exist then save and apply the coupon
            {
                discountAmount = tempDiscountAmount;
                await SaveCouponAsync(cart, coupon, discountAmount);
            }
            return discountAmount;
        }

        public async Task<int> CreateCartAsync(CartDTO cart)
        {
            CartModel cartModel = _mapper.Map<CartModel>(cart);
            return await _cartEFService.AddCartAsync(cartModel);
        }

        public async Task<double> GetCampaignDiscountAsync(CartDTO cart)
        {
            List<CartCampaignDTO> cartCampaignList = _mapper.Map<List<CartCampaignDTO>>(await _cartEFService.GetCartCampaignAsync(cart.CartId));
            return cartCampaignList.Sum(cc => cc.DiscountAmount);
        }

        public async Task<double> GetCouponDiscountAsync(CartDTO cart)
        {
            CartCouponDTO cartCouponDTO = _mapper.Map<CartCouponDTO>(await _cartEFService.GetCartCouponAsync(cart.CartId));
            return cartCouponDTO.DiscountAmount;
        }

        public async Task<double> GetDeliveryCostAsync(CartDTO cart)
        {
            List<DeliveryDTO> deliveryDTOs = _mapper.Map<List<DeliveryDTO>>(await _cartEFService.GetCartDeliveriesAsync(cart.CartId));
            return deliveryDTOs.Sum(d => d.Cost);
        }

        public async Task<double> GetTotalAmountAfterDiscountsAsync(CartDTO cart)
        {
            double totalAmountWithoutDiscounts = 0;
            IList<CartDetailDTO> cartDetailDTOs = await GetCartDetailsAsync(cart.CartId);
            foreach (CartDetailDTO item in cartDetailDTOs)
            {
                totalAmountWithoutDiscounts += (await _productEFService.GetProductAsync(item.ProductId)).Price * item.ProductQuantity;
            }
            return totalAmountWithoutDiscounts - (await GetCampaignDiscountAsync(cart)) - (await GetCouponDiscountAsync(cart));
        }

        public async Task<IList<PrintModel>> PrintAsync(CartDTO cart)
        {
            List<PrintModel> printModels = new List<PrintModel>();
            IList<CartDetailDTO> cartDetailDTOs = await GetCartDetailsAsync(cart.CartId);

            foreach (CartDetailDTO item in cartDetailDTOs)
            {
                ProductDTO productDTO = _mapper.Map<ProductDTO>(await _productEFService.GetProductAsync(item.ProductId));
                CategoryDTO categoryDTO = _mapper.Map<CategoryDTO>(await _categoryEFService.GetCategoryAsync(productDTO.CategoryId));
                PrintModel print = new PrintModel()
                {
                    CategoryName = categoryDTO.Tittle,
                    ProductName = productDTO.Tittle,
                    Quantity = item.ProductQuantity,
                    TotalDiscount = await GetCampaignDiscountAsync(cart) + await GetCouponDiscountAsync(cart),
                    TotalPrice = await GetTotalAmountAfterDiscountsAsync(cart),
                    UnitPrice = productDTO.Price
                };
                printModels.Add(print);
            }
            return printModels;
        }
        public async Task<CartDTO> GetCartAsync(int cartId)
        {
            return _mapper.Map<CartDTO>(await _cartEFService.GetCartAsync(cartId));
        }
        public async Task<CartModel> GetCartModel(int cartId)
        {
            return await _cartEFService.GetCartAsync(cartId);
        }
        public async Task<IList<CartDetailDTO>> GetCartDetailsAsync(int cartId)
        {
            return _mapper.Map<List<CartDetailDTO>>(await _cartEFService.GetCartDetailsAsync(cartId));
        }
        #endregion

        #region Private Methods
        private async Task SaveCampaignAsync(CartDTO cart, CampaignDTO selectedCampaign, double discountAmount)
        {
            CartCampaignDTO cartCampaignDTO = new CartCampaignDTO
            {
                CartId = cart.CartId,
                CampaignId = selectedCampaign.CampaignId,
                DiscountAmount = discountAmount
            };
            CartCampaignModel cartCampaignModel = _mapper.Map<CartCampaignModel>(cartCampaignDTO);
            await _cartEFService.SaveCartCampaignAsync(cartCampaignModel, cart.CartId);
        }



        private async Task<double> CheckCampaignDiscountAmountAsync(CartDTO cart, CampaignDTO campaignItem)
        {
            double discountAmount = 0;
            IList<CartDetailDTO> cartDetailDTOs = await GetCartDetailsAsync(cart.CartId);
            int itemCount = 0;
            double totalProductPriceForCategory = 0;
            foreach (var item in cartDetailDTOs)
            {
                ProductDTO productDTO = _mapper.Map<ProductDTO>(await _productEFService.GetProductAsync(item.ProductId));
                CategoryDTO categoryDTO = _mapper.Map<CategoryDTO>(await _categoryEFService.GetCategoryAsync(productDTO.CategoryId));
                if (categoryDTO.CategoryId == campaignItem.CategoryId)
                {
                    itemCount += item.ProductQuantity;
                    totalProductPriceForCategory += productDTO.Price * item.ProductQuantity;
                }
            }
            if (itemCount >= campaignItem.Quantity)
            {
                switch (campaignItem.DiscountType)
                {
                    case DiscountType.Rate:
                        discountAmount = totalProductPriceForCategory * campaignItem.Discount / 100;
                        break;
                    case DiscountType.Amount:
                        discountAmount = campaignItem.Discount;
                        break;
                    default:
                        break;
                }
            }
            return discountAmount;
        }
        private async Task SaveCouponAsync(CartDTO cart, CouponDTO coupon, double discountAmount)
        {
            CartCouponDTO cartCouponDTO = new CartCouponDTO
            {
                CartId = cart.CartId,
                CouponId = coupon.CouponId,
                DiscountAmount = discountAmount
            };
            CartCouponModel cartCouponModel = _mapper.Map<CartCouponModel>(cartCouponDTO);
            await _cartEFService.SaveCartCouponAsync(cartCouponModel, cart.CartId);
        }



        private async Task<double> CheckCouponDiscountAmountAsync(CartDTO cart, CouponDTO coupon)
        {
            double discountAmount = 0;
            double totalCartCost = 0;
            IList<CartDetailDTO> cartDetailDTOs = await GetCartDetailsAsync(cart.CartId);
            foreach (CartDetailDTO item in cartDetailDTOs)
            {
                ProductDTO productDTO = _mapper.Map<ProductDTO>(await _productEFService.GetProductAsync(item.ProductId));
                totalCartCost += productDTO.Price * item.ProductQuantity;
            }
            if (totalCartCost >= coupon.MinimumCartCost)
            {
                switch (coupon.DiscountType)
                {
                    case DiscountType.Rate:
                        discountAmount = (totalCartCost - await GetCampaignDiscountAsync(cart)) * coupon.Discount / 100;
                        break;
                    case DiscountType.Amount:
                        discountAmount = coupon.Discount;
                        break;
                    default:
                        break;
                }
            }
            return discountAmount;
        }
        #endregion
    }
}
