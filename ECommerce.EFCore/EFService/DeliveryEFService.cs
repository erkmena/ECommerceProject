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
    public class DeliveryEFService : IDeliveryEFService
    {
        private readonly IDbContext _dbContext;
        public DeliveryEFService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> AddDeliveryAsync(DeliveryModel deliveryModel)
        {
            _dbContext.Deliveries.Add(deliveryModel);
            await _dbContext.SaveChangesAsync();
            return deliveryModel.DeliveryId;
        }
        public async Task<IList<DeliveryModel>> GetDeliveriesAsync(int cartId)
        {
            return await _dbContext.Deliveries.Where(d => d.CartId == cartId).ToListAsync();
        }
    }
}
