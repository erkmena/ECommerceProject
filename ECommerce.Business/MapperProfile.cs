using AutoMapper;
using ECommerce.Business.Models.DTO;
using ECommerce.EFCore.Models;

namespace ECommerce.Business
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ProductDTO, ProductModel>();
            CreateMap<ProductModel, ProductDTO>();
            CreateMap<CartDetailDTO, CartDetailModel>();
            CreateMap<CartDetailModel, CartDetailDTO>();
            CreateMap<CartDTO, CartModel>();
            CreateMap<CartModel, CartDTO>();
            CreateMap<CampaignDTO, CampaignModel>();
            CreateMap<CampaignModel, CampaignDTO>();
            CreateMap<CartCampaignDTO, CartCampaignModel>();
            CreateMap<CartCampaignModel, CartCampaignDTO>();
            CreateMap<CartCouponDTO, CartCouponModel>();
            CreateMap<CartDTO, CartModel>();
            CreateMap<CouponDTO, CouponModel>();
            CreateMap<DeliveryDTO, DeliveryModel>();
            CreateMap<CategoryDTO, CategoryModel>();
            CreateMap<CategoryModel, CategoryDTO>();
            CreateMap<CouponModel, CouponDTO>();
            CreateMap<CartCouponModel, CartCouponDTO>();
            CreateMap<DeliveryModel, DeliveryDTO>();
        }
    }
}
