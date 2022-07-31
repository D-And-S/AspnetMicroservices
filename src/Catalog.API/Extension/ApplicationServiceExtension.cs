using Catalog.API.DataContext;
using Catalog.API.Model.IRepository;
using Catalog.API.Model.Repository;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.API.Extension
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<CatalogDbSettings>(config.GetSection("CatalogDataBaseSettings"));
            services.AddSingleton<ICatalogDbSettings>(x => x.GetRequiredService<IOptions<CatalogDbSettings>>().Value);
            services.AddSingleton<IMongoClient>(m => new MongoClient(config.GetValue<string>("CatalogDataBaseSettings:ConnectionString")));
            services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }
    }
}
