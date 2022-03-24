
using ServiceB.API.Repositories;
using ServiceB.API.Repositories.Interfaces;
using DynamicConfigLibrary;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace ServiceB.API.Configuration
{
    public static class ServiceRegistration
    {
        public static void AddConfigApiServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddSingleton(s => new ConfigurationReader(
                configuration.GetValue<string>("ConfigurationSettings:ApplicationName"),
                configuration.GetValue<string>("ConfigurationSettings:ConnectionString"),
                configuration.GetValue<int>("ConfigurationSettings:RefreshTimerIntervalInMs")));
            serviceCollection.AddTransient<IConfigRepository, ConfigRepository>();

        }
    }
}
