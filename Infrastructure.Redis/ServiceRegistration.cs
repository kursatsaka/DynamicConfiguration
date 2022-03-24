using Infrastructure.Redis.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Redis
{
    public static class ServiceRegistration
    {
        public static void AddRedisServices(this IServiceCollection serviceCollection, IConfiguration configuration = null)
        {
            string connectionString = configuration?.GetConnectionString("RedisConnection");

            serviceCollection.AddSingleton(new RedisClient(connectionString));
        }
    }
}
