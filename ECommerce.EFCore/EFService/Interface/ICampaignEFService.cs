using ECommerce.EFCore.Models;
using System.Threading.Tasks;

namespace ECommerce.EFCore.EFService.Interface
{
    public interface ICampaignEFService
    {
        Task<int> AddCampaignAsync(CampaignModel campaignModel);
    }
}
