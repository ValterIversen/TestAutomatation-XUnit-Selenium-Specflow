using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestAutomatation.Domain.Interface;
using TestAutomatation.Infra.Data.Context;
using TestAutomatation.Infra.Data.Repositories;

namespace TestAutomatation.IoC
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegistrarDependencias(this IServiceCollection services, IConfiguration config)
        {
            return services
                .RegistrarDbContext(config)
                .RegistrarRepositorios();
        }

        private static IServiceCollection RegistrarDbContext(this IServiceCollection services, IConfiguration config)
        {
            return services.AddDbContext<Infra.Data.Context.AppContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("App"));
            });
        }

        private static IServiceCollection RegistrarRepositorios(this IServiceCollection service)
        {
            return service
                .AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
