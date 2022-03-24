using Infrastructure.Redis.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicConfigLibrary
{
    public class ConfigurationReader : IConfigurationReader
    {
        private readonly string _applicationName;
        private readonly string _connectionString;
        private readonly int _refreshTimerIntervalInMs;
        private readonly RedisClient _redisClient;
        private readonly IMemoryCache _memoryCache;
        private readonly IServiceProvider _provider;


        public ConfigurationReader(string applicationName, string connectionString, int refreshTimerIntervalInMs)
        {
            _applicationName = applicationName;
            _connectionString = connectionString;
            _refreshTimerIntervalInMs = refreshTimerIntervalInMs;
            _redisClient = new RedisClient(connectionString);
            _memoryCache = _provider.GetRequiredService<IMemoryCache>();
        }

        public async Task<T> GetValue<T>(string key)
        {
            T response;
            try
            {
                response = JsonConvert.DeserializeObject<T>(await _redisClient.StringGetAsync(_applicationName + "/" + key));
                _memoryCache.Set(key, response);        //set item to memorycache
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                response = _memoryCache.Get<T>(key); // get item in memorycache
            }
            return response;
        }

        public bool SetValue<T>(string key, T value)
        {
            return _redisClient.StringSet(_applicationName + "/" + key, JsonConvert.SerializeObject(value));
        }
    }
}
