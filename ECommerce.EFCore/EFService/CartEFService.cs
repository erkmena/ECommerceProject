using ECommerce.EFCore.Data;
using ECommerce.EFCore.EFService.Interface;
using ECommerce.EFCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.EFCore.EFService
{

    public class CartEFService : ICartEFService
    {
        private IDbContext _dbContext;
        public CartEFService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> AddCartDetailAsync(CartDetailModel cartDetailModel)
        {
            _dbContext.CartDetailModels.Add(cartDetailModel);
            await _dbContext.SaveChangesAsync();
            return cartDetailModel.CartDetailId;
        }
        public async Task<int> AddCartAsync(CartModel cartModel)
        {
            _dbContext.Carts.Add(cartModel);
             await _dbContext.SaveChangesAsync();
            return cartModel.CartId;
        }
        public async Task<IList<CartCampaignModel>> GetCartCampaignAsync(int cartId)
        {
            return await _dbContext.CartCampaigns.Where(cc => cc.CartId == cartId).ToListAsync();
        }

        public async Task<CartCouponModel> GetCartCouponAsync(int cartId)
        {
            return await _dbContext.CartCoupons.Where(cc => cc.CartId == cartId).FirstOrDefaultAsync();
        }
        public async Task<IList<DeliveryModel>> GetCartDeliveriesAsync(int cartId)
        {
            return await _dbContext.Deliveries.Where(d => d.CartId == cartId).ToListAsync();
        }
        public async Task<CartModel> GetCartAsync(int cartId)
        {
            return await _dbContext.Carts.AsNoTracking().Where(c => c.CartId == cartId).FirstOrDefaultAsync();
        }
        public async Task SaveCartCampaignAsync(CartCampaignModel cartCampaignModel, int cartId)
        {
            if (_dbContext.CartCampaigns.Count(cc => cc.CartId == cartId) > 0)
            {
                _dbContext.CartCampaigns.RemoveRange(_dbContext.CartCampaigns.Where(cc => cc.CartId == cartId)); //Remove old campaigns from cart
            }
            _dbContext.CartCampaigns.Add(cartCampaignModel); //Add new selected campaign
            await _dbContext.SaveChangesAsync();
        }
        public async Task SaveCartCouponAsync(CartCouponModel cartCouponModel, int cartId)
        {
            if (_dbContext.CartCoupons.Count(cc => cc.CartId == cartId) > 0)
            {
                _dbContext.CartCoupons.Remove(_dbContext.CartCoupons.Where(cc => cc.CartId == cartId).FirstOrDefault()); //Remove old coupon from cart
            }
            _dbContext.CartCoupons.Add(cartCouponModel); //Add new coupon
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IList<CartDetailModel>> GetCartDetailsAsync(int cartId)
        {
            return await _dbContext.CartDetailModels.Where(c => c.CartId == cartId).ToListAsync();
        }
    }
}
