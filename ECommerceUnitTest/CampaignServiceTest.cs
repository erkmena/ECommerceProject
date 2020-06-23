using AutoMapper;
using ECommerce.Business;
using ECommerce.Business.Models.DTO;
using ECommerce.Business.Services;
using ECommerce.EFCore.EFService.Interface;
using ECommerce.EFCore.Models;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace ECommerce.UnitTest
{
    public class CampaignServiceTest
    {
        private IMapper _mapper;
        private Mock<ICampaignEFService> campaignEFServiceMock;
        private Campaign campaign;
        [SetUp]
        public void Setup()
        {
            campaignEFServiceMock = new Mock<ICampaignEFService>();
            campaignEFServiceMock.Setup(c => c.AddCampaignAsync(It.IsAny<CampaignModel>())).Returns(Task.FromResult(MockModels.MockCampaignModel().CampaignId));

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile());
            });
            _mapper = mapperConfig.CreateMapper();

            campaign = new Campaign(_mapper, campaignEFServiceMock.Object);

        }

        [Test]
        public async Task CreateCampaignTest()
        {
            int campaignID = await campaign.CreateCampaignAsync(_mapper.Map<CampaignDTO>(MockModels.MockCampaignModel()));
            Assert.AreEqual(campaignID, MockModels.MockCampaignModel().CampaignId);
        }
    }
}
