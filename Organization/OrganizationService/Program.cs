using Engine.Organization;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;
using Topshelf;
namespace OrganizationService
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
           .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            HostFactory.Run(f =>
            {
                f.Service<Service>();
                f.SetServiceName("DynamicConfiguration.OrganizationService");
                f.StartAutomatically();
                f.RunAsLocalSystem();
            });
        }
    }
}
