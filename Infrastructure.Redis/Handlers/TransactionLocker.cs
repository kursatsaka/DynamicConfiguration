
using Infrastructure.Redis.Services;

namespace Infrastructure.Redis.Handlers
{
    public class TransactionLocker
    {
        private readonly RedisCacheKeyHandler redisCacheKeyHandler;
        private readonly RedisClient redisClient;

        public TransactionLocker(RedisCacheKeyHandler redisCacheKeyHandler, RedisClient redisClient)
        {
            this.redisCacheKeyHandler = redisCacheKeyHandler;
            this.redisClient = redisClient;
        }

    }
}
