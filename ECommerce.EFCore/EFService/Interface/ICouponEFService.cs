using ECommerce.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.EFCore.EFService.Interface
{
    public interface ICouponEFService
    {
        Task<int> AddCouponAsync(CouponModel couponModel);
        Task<CouponModel> GetCouponAsync(int couponId);
    }
}
