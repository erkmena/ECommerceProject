using ECommerce.EFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.EFCore.Data
{
    public interface IDbContext
    {
        public IConfiguration Configuration { get; }
        public DbSet<CampaignModel> Campaigns { get; set; }
        public DbSet<CampaignProductTypeModel> CampaignProductTypes { get; set; }
        public DbSet<CartCouponModel> CartCoupons { get; set; }
        public DbSet<CartCampaignModel> CartCampaigns { get; set; }
        public DbSet<CartDetailModel> CartDetailModels { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<CouponModel> Coupons { get; set; }
        public DbSet<CartModel> Carts { get; set; }
        public DbSet<DeliveryModel> Deliveries { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<ProductTypeModel> ProductTypes { get; set; }
        public int SaveChanges();
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
