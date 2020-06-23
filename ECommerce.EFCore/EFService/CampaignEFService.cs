using ECommerce.EFCore.Data;
using ECommerce.EFCore.EFService.Interface;
using ECommerce.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.EFCore.EFService
{
    public class CampaignEFService : ICampaignEFService
    {
        private readonly IDbContext _dbContext;
        public CampaignEFService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> AddCampaignAsync(CampaignModel campaignModel)
        {
            _dbContext.Campaigns.Add(campaignModel);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
