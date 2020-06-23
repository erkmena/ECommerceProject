using AutoMapper;
using ECommerce.Business;
using ECommerce.Business.Services;
using ECommerce.Business.Services.Interfaces;
using ECommerce.EFCore.Data;
using ECommerce.EFCore.EFService;
using ECommerce.EFCore.EFService.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace Ecommerce.WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllersWithViews();

            services.AddScoped<ICart, Cart>();
            services.AddScoped<IDelivery, Delivery>();
            services.AddScoped<ICategory, Category>();
            services.AddScoped<IProduct, Product>();
            services.AddScoped<ICampaign, Campaign>();
            services.AddScoped<ICoupon, Coupon>();
            services.AddScoped<IDelivery, Delivery>();
            services.AddScoped<IDbContext, ApplicationDbContext>();
            services.AddScoped<ICartEFService, CartEFService>();
            services.AddScoped<IDeliveryEFService, DeliveryEFService>();
            services.AddScoped<ICategoryEFService, CategoryEFService>();
            services.AddScoped<IProductEFService, ProductEFService>();
            services.AddScoped<ICampaignEFService, CampaignEFService>();
            services.AddScoped<ICouponEFService, CouponEFService>();
            services.AddScoped<IDeliveryEFService, DeliveryEFService>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
