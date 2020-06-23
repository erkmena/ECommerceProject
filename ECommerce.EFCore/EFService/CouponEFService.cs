using ECommerce.EFCore.Data;
using ECommerce.EFCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.EFCore.EFService.Interface
{
    public class CouponEFService : ICouponEFService
    {
        private readonly IDbContext _dbContext;
        public CouponEFService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> AddCouponAsync(CouponModel couponModel)
        {
            _dbContext.Coupons.Add(couponModel);
            await _dbContext.SaveChangesAsync();
            return couponModel.CouponId;
        }

        public async Task<CouponModel> GetCouponAsync(int couponId)
        {
            return await _dbContext.Coupons.Where(c => c.CouponId == couponId).FirstOrDefaultAsync();
        }
    }
}
