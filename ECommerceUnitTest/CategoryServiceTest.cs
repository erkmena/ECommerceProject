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
    public class CategoryServiceTest
    {
        private Category category;
        private IMapper _mapper;
        private Mock<ICategoryEFService> categoryEFServiceMock;
        private const int MockCategoryId = 1;
        [SetUp]
        public void Setup()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile());
            });
            _mapper = mapperConfig.CreateMapper();

            categoryEFServiceMock = new Mock<ICategoryEFService>();
            categoryEFServiceMock.Setup(cef => cef.AddCategoryAsync(It.IsAny<CategoryModel>())).Returns(Task.FromResult(MockCategoryId));
            categoryEFServiceMock.Setup(cef => cef.GetCategoryAsync(It.IsAny<int>())).Returns(Task.FromResult(MockModels.MockCategoryModel()));

            category = new Category(_mapper, categoryEFServiceMock.Object);
        }

        [Test]
        public async Task CreateNewCategoryTest()
        {
            CategoryDTO categoryDTO = _mapper.Map<CategoryDTO>(MockModels.MockCategoryModel());
            int categoryId = await category.CreateNewCategoryAsync(categoryDTO);
            Assert.AreEqual(categoryId, MockCategoryId);
        }
        [Test]
        public async Task GetCategoryTest()
        {
            CategoryDTO categoryDTO = await category.GetCategoryAsync(1);
            Assert.IsTrue(categoryDTO != null && categoryDTO.CategoryId == MockCategoryId);
        }
    }
}
