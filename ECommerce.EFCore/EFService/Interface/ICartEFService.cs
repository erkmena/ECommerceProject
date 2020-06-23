using ECommerce.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.EFCore.EFService.Interface
{
    public interface ICartEFService
    {
        Task<int> AddCartDetailAsync(CartDetailModel cartDetailModel);
        Task<int> AddCartAsync(CartModel cartModel);
        Task<IList<CartCampaignModel>> GetCartCampaignAsync(int cartId);
        Task<CartCouponModel> GetCartCouponAsync(int cartId);
        Task<IList<DeliveryModel>> GetCartDeliveriesAsync(int cartId);
        Task<CartModel> GetCartAsync(int cartId);
        Task SaveCartCampaignAsync(CartCampaignModel cartCampaignModel, int cartId);
        Task SaveCartCouponAsync(CartCouponModel cartCouponModel, int cartId);
        Task<IList<CartDetailModel>> GetCartDetailsAsync(int cartId);
    }
}
