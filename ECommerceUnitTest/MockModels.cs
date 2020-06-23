using ECommerce.Business.Services;
using ECommerce.EFCore.EFService.Interface;
using ECommerce.EFCore.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UnitTest
{
    public static class MockModels
    {
        public static CartModel MockCartModel()
        {
            CartModel cartModel = new CartModel()
            {
                CustomerId = 0,
            };
            return cartModel;
        }
        public static IList<CartDetailModel> MockListCartDetailModel()
        {
            List<CartDetailModel> cartDetailModels = new List<CartDetailModel>() { MockCartDetailModel(), MockCartDetailModel() };
            return cartDetailModels;
        }

        public static CartDetailModel MockCartDetailModel()
        {
            CartDetailModel cartDetailModel = new CartDetailModel()
            {
                ProductId = MockProductModel().ProductId,
                ProductQuantity = 3
            };
            return cartDetailModel;
        }
        public static ProductModel MockProductModel()
        {
            ProductModel productModel = new ProductModel()
            {
                CategoryId = MockCategoryModel().CategoryId,
                Price = 100,
                ProductId = 10,
                Tittle = "TestProduct"
            };
            return productModel;
        }
        public static CategoryModel MockCategoryModel()
        {
            CategoryModel categoryModel = new CategoryModel()
            {
                CategoryId = 1,
                Tittle = "TestCategory"
            };
            return categoryModel;
        }

        public static IList<DeliveryModel> MockCartDeliveries()
        {
            List<DeliveryModel> deliveryModels = new List<DeliveryModel>() { MockCartDeliveryModel(), MockCartDeliveryModel() };
            return deliveryModels;
        }

        public static DeliveryModel MockCartDeliveryModel()
        {
            DeliveryModel deliveryModel = new DeliveryModel()
            {
                CartId = MockCartModel().CartId,
                Cost = 70,
                DeliveryId = 1000
            };
            return deliveryModel;
        }

        public static CartCouponModel MockCartCouponModel()
        {
            CartCouponModel cartCouponModel = new CartCouponModel()
            {
                CartId = MockCartModel().CartId,
                CouponId = MockCouponModel().CouponId,
                DiscountAmount = 100
            };
            return cartCouponModel;
        }

        public static CouponModel MockCouponModel()
        {
            CouponModel couponModel = new CouponModel()
            {
                CouponId = 0,
                DiscountRate = 10,
                MinimumCartCost = 500,
                Discount = 100
            };
            return couponModel;
        }

        public static IList<CartCampaignModel> MockListCartCampaignModel()
        {
            List<CartCampaignModel> campaignModels = new List<CartCampaignModel>() { MockCartCampaignModel(), MockCartCampaignModel(), MockCartCampaignModel() };
            return campaignModels;
        }
        public static CartCampaignModel MockCartCampaignModel()
        {
            CartCampaignModel cartCampaignModel = new CartCampaignModel()
            {
                DiscountAmount = 50,
                CampaignId = MockCampaignModel().CampaignId,
                CartId = MockCartModel().CartId
            };
            return cartCampaignModel;
        }

        public static CampaignModel MockCampaignModel()
        {
            CampaignModel campaignModel = new CampaignModel()
            {
                CampaignId = 100,
                Discount = 10,
                DiscountType = 0,
                CategoryId = MockCategoryModel().CategoryId,
                Quantity = 1,
                Tittle = "TestCampaign",
            };
            return campaignModel;
        }
    }
}
