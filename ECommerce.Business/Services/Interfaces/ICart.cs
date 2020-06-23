using ECommerce.Business.Models;
using ECommerce.Business.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Business.Services.Interfaces
{
    public interface ICart
    {
        /// <summary>
        /// Creates a new Shopping Cart and return CartId
        /// </summary>
        /// <param name="cart">The Shopping Cart Model</param>
        /// <returns></returns>
        Task<int> CreateCartAsync(CartDTO cart);
        /// <summary>
        /// Returns Total Amount of Shopping Cart after Discounts applied.
        /// </summary>
        /// <param name="cart">The Shopping Cart Model</param>
        /// <returns></returns>
        Task<double> GetTotalAmountAfterDiscountsAsync(CartDTO cart);
        /// <summary>
        /// Returns the discount amount of Coupon applied in cart
        /// </summary>
        /// <param name="cart">The Shopping Cart Model</param>
        /// <returns></returns>
        Task<double> GetCouponDiscountAsync(CartDTO cart);
        /// <summary>
        /// Returns the discount amount of Campaign applied in cart
        /// </summary>
        /// <param name="cart">The Shopping Cart Model</param>
        /// <returns></returns>
        Task<double> GetCampaignDiscountAsync(CartDTO cart);
        /// <summary>
        /// Returns the delivery cost of shopping cart
        /// </summary>
        /// <param name="cart">The Shopping Cart Model</param>
        /// <returns></returns>
        Task<double> GetDeliveryCostAsync(CartDTO cart);
        /// <summary>
        /// Apply and save Coupon to Shopping Cart and return discount amount of applied Coupon
        /// </summary>
        /// <param name="cart">The Shopping Cart Model</param>
        /// <param name="coupon">The Coupon that applied</param>
        /// <returns></returns>
        Task<double> ApplyCouponDiscountAsync(CartDTO cart,CouponDTO coupon);
        /// <summary>
        /// Choose Campaign which has most discount and Apply and Save that Campaign to Shopping Cart and return discount amount of applied Campaign
        /// </summary>
        /// <param name="cart">The Shopping Cart Model</param>
        /// <param name="campaignList">List of available campaigns</param>
        /// <returns></returns>
        Task<double> ApplyCampaignDiscountAsync(CartDTO cart,IList<CampaignDTO> campaignList);
        /// <summary>
        /// Add new product to Shopping Cart
        /// </summary>
        /// <param name="cartId">Shopping CartId which Product will be added</param>
        /// <param name="product">The Product Model which will be added</param>
        /// <param name="quantity">Quantity of Product to add Shopping Cart</param>
        /// <returns></returns>
        Task<int> AddItemAsync(int cartId,ProductDTO product, int quantity);
        /// <summary>
        /// The Print Model For Shopping Cart to show which products, unit prices ,quantities and categories of products added to Shopping Cart and total Amount, Discount amount
        /// </summary>
        /// <param name="cart">The Shopping Cart Model</param>
        /// <returns></returns>
        Task<IList<PrintModel>> PrintAsync(CartDTO cart);
        /// <summary>
        /// Returns the Shopping Cart Model
        /// </summary>
        /// <param name="cartId">Id Of Shopping Cart</param>
        /// <returns></returns>
        Task<CartDTO> GetCartAsync(int cartId);
        /// <summary>
        /// Returns the Products and Quantity Of Products of added in Shopping Cart
        /// </summary>
        /// <param name="cartId">Shopping Cart Id</param>
        /// <returns></returns>
        Task<IList<CartDetailDTO>> GetCartDetailsAsync(int cartId);
    }
}
