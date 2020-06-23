using AutoMapper;
using ECommerce.Business.Models.DTO;
using ECommerce.Business.Services.Interfaces;
using ECommerce.EFCore.EFService.Interface;
using ECommerce.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Business.Services
{
    public class Delivery : IDelivery
    {
        private ICartEFService _cartEFService;
        private IDeliveryEFService _deliveryEFService;
        private IProductEFService _productEFService;
        private ICategoryEFService _categoryEFService;
        private IMapper _mapper;
        public Delivery(IDeliveryEFService deliveryEFService,ICartEFService cartEFService,IProductEFService productEFService, ICategoryEFService categoryEFService, IMapper mapper)
        {
            _deliveryEFService = deliveryEFService;
            _cartEFService = cartEFService;
            _mapper = mapper;
            _productEFService = productEFService;
            _categoryEFService = categoryEFService;
        }
        public async Task<double> CalculateDeliveryCostAndCountAsync(int cartId, double costPerDelivery, double costPerProduct, double fixedCost)
        {
            Cart cart = new Cart(_cartEFService, _mapper,_productEFService,_categoryEFService);
            IList<CartDetailDTO> cartDetailDTOs = await cart.GetCartDetailsAsync(cartId);  
            int numberOfDeliveries = cartDetailDTOs.Select(async cd => (await _productEFService.GetProductAsync(cd.ProductId)).CategoryId).Distinct().Count();
            double deliveryCost = (costPerDelivery * numberOfDeliveries) + (costPerProduct * cartDetailDTOs.Select(cd => cd.ProductId).Distinct().Count()) + fixedCost;
            deliveryCost = Math.Round(deliveryCost, 2);
            DeliveryDTO deliveryDTO = new DeliveryDTO()
            {
                CartId = cartId,
                Cost = deliveryCost
            };
            await _deliveryEFService.AddDeliveryAsync(_mapper.Map<DeliveryModel>(deliveryDTO));
            return deliveryCost;
        }
        public async Task<double> GetDeliveryCostAsync(CartDTO cart)
        {
            return (await _deliveryEFService.GetDeliveriesAsync(cart.CartId)).Sum(d => d.Cost);
        }
    }
}
