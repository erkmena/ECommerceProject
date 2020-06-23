using ECommerce.Business.Models.DTO;
using System.Threading.Tasks;

namespace ECommerce.Business.Services.Interfaces
{
    public interface ICoupon
    {
        /// <summary>
        /// Create And Save the Coupon To DB and Returns new CouponId
        /// </summary>
        /// <param name="category">Category Model To Added</param>
        /// <returns></returns>
        Task<int> CreateNewCouponAsync(CouponDTO category);
        /// <summary>
        /// Return the Coupon of given couponId
        /// </summary>
        /// <param name="couponId">Coupon Id</param>
        /// <returns></returns>
        Task<CouponDTO> GetCouponAsync(int couponId);
    }
}
