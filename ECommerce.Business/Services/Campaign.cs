using AutoMapper;
using ECommerce.Business.Models.DTO;
using ECommerce.Business.Services.Interfaces;
using ECommerce.EFCore.EFService.Interface;
using ECommerce.EFCore.Models;
using System.Threading.Tasks;

namespace ECommerce.Business.Services
{
    public class Campaign : ICampaign
    {
        private IMapper _mapper;
        private ICampaignEFService _campaignEFService;

        public Campaign(IMapper mapper, ICampaignEFService campaignEFService)
        {
            _mapper = mapper;
            _campaignEFService = campaignEFService;
        }
        public async Task<int> CreateCampaignAsync(CampaignDTO campaign)
        {
            CampaignModel campaignModel = _mapper.Map<CampaignModel>(campaign);
            return await _campaignEFService.AddCampaignAsync(campaignModel);
        }
    }
}
