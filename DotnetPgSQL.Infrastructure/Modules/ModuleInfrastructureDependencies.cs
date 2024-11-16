using DotnetPgSQL.Application.Contracts.Persistence;
using DotnetPgSQL.Application.Contracts.Services;
using DotnetPgSQL.Infrastructure.Repositories;
using DotnetPgSQL.Infrastructure.Services;
using DotnetPgSQL.Infrastructure.UnitOfWorkPattern;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetPgSQL.Infrastructure.Modules
{
    public static class ModuleInfrastructureDependencies
    {
        public static void AddInfrastructureDependencies(this IServiceCollection services)
        {
            // Register Unit Of Work and Generic Repository and Services
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
        }
    }
}