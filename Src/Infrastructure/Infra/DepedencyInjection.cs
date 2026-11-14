using Application.Interfaces.Events;
using Application.Interfaces.Products;
using Application.Interfaces.Uof;
using Infra.Data.DbContexts;
using Infra.Events;
using Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra
{
    public static class DepedencyInjection
    {
        public static IServiceCollection InfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DemoDbContext>(config => {
                config.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IProductReadRepository, ProductReadRepo>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepo>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDomainEventsDispatcher, EventHandlerDispatcher>();
            return services;
        }
    }
}
