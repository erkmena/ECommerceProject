using AutoMapper;
using ECommerce.Business.Models.DTO;
using ECommerce.Business.Services.Interfaces;
using ECommerce.EFCore.EFService.Interface;
using ECommerce.EFCore.Models;
using System.Threading.Tasks;

namespace ECommerce.Business.Services
{
    public class Coupon : ICoupon
    {
        private IMapper _mapper;
        private ICouponEFService _couponEFService;

        public Coupon(IMapper mapper, ICouponEFService couponEFService)
        {
            _mapper = mapper;
            _couponEFService = couponEFService;
        }
        public async Task<int> CreateNewCouponAsync(CouponDTO coupon)
        {
            return await _couponEFService.AddCouponAsync(_mapper.Map<CouponModel>(coupon));
        }

        public async Task<CouponDTO> GetCouponAsync(int couponId)
        {
            return _mapper.Map<CouponDTO>(await _couponEFService.GetCouponAsync(couponId));
        }
    }
}
