using Engine.Organization.Factories;
using Engine.Organization.Handlers;
using Engine.Organization.Jobs;
using Infrastructure.Redis;
using Infrastructure.Redis.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Engine.Organization.Configuration
{
    public class InjectionConfiguration
    {
        public ServiceCollection RegisterContainer(IConfigurationRoot configuration)
        {
            ServiceCollection serviceCollection = new ServiceCollection();

            // Build configuration
            configuration = new ConfigurationBuilder()
            .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            serviceCollection.AddSingleton<IConfigurationRoot>(configuration);

            #region QuartzService
            serviceCollection.AddSingleton<IJobFactory, JobFactory>();
            serviceCollection.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            serviceCollection.AddSingleton<QuartzJobRunner>();
            serviceCollection.AddSingleton<JobRegistrar>();
            #endregion

            var jobAssembly = typeof(BaseJob).Assembly;

            var jobs =
                from type in jobAssembly.GetExportedTypes()
                where type.Namespace == "Engine.Organization.Jobs"
                where type.Name != "BaseJob"
                select new { Implementation = type };

            foreach (var job in jobs)
            {
                serviceCollection.AddTransient(job.Implementation);
            }
           
            serviceCollection.AddRedisServices(configuration);

            return serviceCollection;

        }
    }
}
