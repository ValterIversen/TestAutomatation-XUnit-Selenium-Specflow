using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolidToken.SpecFlow.DependencyInjection;
using TestAutomatation.IoC;
using TestAutomatation.SiteChacara.Validations;

namespace TestAutomatation.SiteChacara.Config
{
    public static class ConfiguracaoDependencias
    {
        [ScenarioDependencies]
        public static IServiceCollection CreateServices()
        {
            var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

            var services = new ServiceCollection()
                    .RegistrarDependencias(config)
                    .AddScoped<SCFixture>()
                    .AddScoped<CommonValidations>();
            return services;
        }
    }
}