using AutoMapper;
using CustomersOrderOtomation.Data.Models;
using CustomersOrderOtomation.Data.Repository.Abstract;
using CustomersOrderOtomation.Data.Repository.Concrete;
using CustomersOrderOtomation.Data.UnitOfWork.Abstract;
using CustomersOrderOtomation.Data.UnitOfWork.Concrete;
using CustomersOrderOtomation.Service.Abstract;
using CustomersOrderOtomation.Service.Concrete;
using MyProductList.Service.Mapper;

namespace CustomersOrderOtomation.Extensions
{
    public static class StartUpDIExtension
    {
        public static void AddServicesDI(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IGenericRepository<Category>, GenericRepository<Category>>();
            services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
            services.AddScoped<IGenericRepository<ShopList>, GenericRepository<ShopList>>();


            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IShopListService, ShopListService>();


            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IShopListRepository, ShopListRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            // mapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            services.AddSingleton(mapperConfig.CreateMapper());
        }


    }
}
