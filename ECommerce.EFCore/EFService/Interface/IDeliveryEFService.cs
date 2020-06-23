using ECommerce.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.EFCore.EFService.Interface
{
    public interface IDeliveryEFService
    {
        Task<int> AddDeliveryAsync(DeliveryModel deliveryModel);
        Task<IList<DeliveryModel>> GetDeliveriesAsync(int cartId);
    }
}
