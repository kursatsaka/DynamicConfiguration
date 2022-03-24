using Engine.Organization.Configuration;
using Engine.Organization.Factories;
using Engine.Organization.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Specialized;
using Topshelf;

namespace Engine.Organization
{
    public class Service : ServiceControl
    {
        private IScheduler scheduler;
        private JobRegistrar _jobRegistrar;
        public static IConfigurationRoot configuration;
        public bool Start(HostControl hostControl)
        {
            try
            {
                var serviceCollection = new InjectionConfiguration().RegisterContainer(configuration);

                var props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" }
            };
                var factory = new StdSchedulerFactory(props);
                var sched = factory.GetScheduler().Result;
                var serviceProvider = serviceCollection.BuildServiceProvider();
                sched.JobFactory = new JobFactory(serviceProvider);

                sched.Start().Wait();

                _jobRegistrar = serviceCollection.BuildServiceProvider().GetService<JobRegistrar>();
                _jobRegistrar.RegisterJobs(sched);

                //log.Info("OrganizationService started");

                return true;
            }
            catch (Exception ex)
            {
                //Log.Error(ex, "Error occured in Service Start");

                return false;
            }
        }

        public bool Stop(HostControl hostControl)
        {
            //log.Warn("OrganizationService stopped");

            scheduler.Shutdown().Wait();

            return true;
        }
    }
}
