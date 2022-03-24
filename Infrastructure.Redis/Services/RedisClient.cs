using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Redis.Services
{
    public class RedisClient
    {
        protected IDatabase Database;
        private string _connectionString;

        public RedisClient(string connectionString)
        {
            _connectionString = connectionString;
            Database = ConnectionMultiplexer.Connect(_connectionString).GetDatabase();
        }

        public bool AddListITem(string listKey, string value)
        {
            return Database.ListLeftPush(listKey, value) > 0;
        }

        public string GetListItem(string listKey)
        {
            return Database.ListRightPop(listKey);
        }

        public string StringGet(string key)
        {
            return Database.StringGet(key);
        }

        public async Task<string> StringGetAsync(string key)
        {
            return await Database.StringGetAsync(key);
        }

        public bool StringSet(string key, string value)
        {
            return Database.StringSet(key, value);
        }

        public bool StringSet(string key, string value, int hour)
        {
            return Database.StringSet(key, value, TimeSpan.FromHours(hour));
        }

        public void Delete(string key)
        {
            Database.KeyDelete(key);
        }

        public bool HasKey(string key)
        {
            return Database.KeyExists(key);
        }

        public ITransaction CreateTransaction()
        {
            return Database.CreateTransaction();
        }

        public Task<RedisValue> GetBytes(string key)
        {
            return Database.StringGetAsync(key);
        }

        public async Task SortedSetAdd(string key, RedisValue value, double version)
        {
            await Database.SortedSetAddAsync(key, value, version);
        }

        public async Task ExpireSortedSet(string key, double version)
        {
            await Database.SortedSetRemoveRangeByScoreAsync(key, 0, version, Exclude.Stop);
        }

        public bool SortedSetHasMember(string key, string value, double requiredScore = 0d)
        {
            var currentScore = Database.SortedSetScore(key, value);
            return currentScore.HasValue && currentScore >= requiredScore;
        }

        public bool StringSetNotExists(string key, string value)
        {
            return Database.StringSet(key, value, TimeSpan.FromMinutes(5), When.NotExists, CommandFlags.DemandMaster);
        }
    }
}
