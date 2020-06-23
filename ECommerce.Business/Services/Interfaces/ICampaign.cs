using ECommerce.Business.Models.DTO;
using System.Threading.Tasks;

namespace ECommerce.Business.Services.Interfaces
{
    public interface ICampaign
    {
        /// <summary>
        /// Creates a new Campaign and return CampaignId
        /// </summary>
        /// <param name="campaign">The Campaign Model</param>
        /// <returns></returns>
        Task<int> CreateCampaignAsync(CampaignDTO campaign);
    }
}
