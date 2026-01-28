using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ReservasCanchas.DataAccess.Repositories
{
    public class RedisRepository
    {
        private readonly IDatabase _redisDb;

        public RedisRepository(IConnectionMultiplexer redis)
        {
            _redisDb = redis.GetDatabase();
        }

        public async Task<bool> LockSlotIfNotExistAsync(string lockKey, string value, TimeSpan expiry)
        {
            return await _redisDb.StringSetAsync(lockKey, value, expiry, When.NotExists);
        }

        public async Task<bool> SetCheckoutContextAsync<T>(string checkoutKey, T obj, TimeSpan expiry)
        {
            var stringToJson = JsonSerializer.Serialize(obj);
            return await _redisDb.StringSetAsync(checkoutKey, stringToJson, expiry);
        }

        public async Task<bool> SetUserIdAsync(string userIdKey, string value, TimeSpan expiry)
        {
            return await _redisDb.StringSetAsync(userIdKey, value, expiry);
        }

        public async Task<string?> GetValueAsync(string key)
        {
            return await _redisDb.StringGetAsync(key);
        }

        public async Task DeleteKeyAsync(string key)
        {
            await _redisDb.KeyDeleteAsync(key);
        }


    }
}
