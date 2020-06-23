using Microsoft.EntityFrameworkCore;
using ECommerce.EFCore.Models;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace ECommerce.EFCore.Data
{
    public class ApplicationDbContext : DbContext, IDbContext
    {
        public IConfiguration Configuration { get; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }
        public DbSet<CartCampaignModel> CartCampaigns { get; set; }
        public DbSet<CampaignModel> Campaigns { get; set; }
        public DbSet<CampaignProductTypeModel> CampaignProductTypes { get; set; }
        public DbSet<CartCouponModel> CartCoupons { get; set; }
        public DbSet<CartDetailModel> CartDetailModels { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<CouponModel> Coupons { get; set; }
        public DbSet<CartModel> Carts { get; set; }
        public DbSet<DeliveryModel> Deliveries { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<ProductTypeModel> ProductTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CampaignModel>().ToTable("Campaign");
            modelBuilder.Entity<CampaignProductTypeModel>().ToTable("CampaignProductType");
            modelBuilder.Entity<CartCouponModel>().ToTable("CartCoupon");
            modelBuilder.Entity<CartDetailModel>().ToTable("CartDetail");
            modelBuilder.Entity<CouponModel>().ToTable("Coupon");
            modelBuilder.Entity<CartModel>().ToTable("Cart");
            modelBuilder.Entity<DeliveryModel>().ToTable("Delivery");
            modelBuilder.Entity<ProductModel>().ToTable("Product");
            modelBuilder.Entity<ProductTypeModel>().ToTable("ProductType");
            modelBuilder.Entity<CategoryModel>().ToTable("Category");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }

        public void DetachAllEntities()
        {
            var changedEntriesCopy = this.ChangeTracker.Entries().Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }
    }
}