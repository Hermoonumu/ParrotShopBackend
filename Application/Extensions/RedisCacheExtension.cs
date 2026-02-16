
using System.Reflection;
using ParrotShopBackend.Application.DTO;
using ParrotShopBackend.Domain;
using StackExchange.Redis;

namespace ParrotShopBackend.Application.Extensions;


public class RedisCacheExtension
{
    private IConnectionMultiplexer _muxer;
    public IDatabase _redis;
    public RedisCacheExtension(IConnectionMultiplexer muxer)
    {
        _muxer = muxer;
        _redis = _muxer.GetDatabase();
    }


    public async Task SetStringAsync(string key, string value, TimeSpan? ttl=null)
    {
        await _redis.StringSetAsync(key, value, (Expiration)ttl!);
    }

    public async Task<string?> GetStringAsync(string key)
    {
        return await _redis.StringGetAsync(key);
    }

    public async Task SADD(string key, long value)
    {
        await _redis.SetAddAsync(key, value);
    }

    public async Task SREM(string key, long value)
    {
        await _redis.SetRemoveAsync(key, value);
    }

    public async Task<List<long>> ApplyFilterAsync(ParrotFilterDTO pfDTO)
    {
        PropertyInfo[] Filters = pfDTO.GetType().GetProperties();
        
        List<string> RedisPreparedFilters = [];
        foreach (PropertyInfo pi in Filters)
        {
            if (pi.Name == "PriceFrom" || pi.Name == "PriceTo" || pi.Name == "AscendingPrice")
                continue;
            if (pi.GetValue(pfDTO) is null) continue;
            if (pi.Name == "Color")
            {
                foreach (Color col in pi.GetValue(pfDTO) as List<Color>)
                {
                    RedisPreparedFilters.Add($"{pi.Name}_{col}");
                }
                continue;
            }
            RedisPreparedFilters.Add($"{pi.Name}_{pi.GetValue(pfDTO)}");
        }
        
        RedisKey[] keys = RedisPreparedFilters.Select(x => (RedisKey)x).ToArray();
        
        RedisValue[] result = await _redis.SetCombineAsync(SetOperation.Intersect, keys);
        
        return [.. result.Select(x => (long)x)];
    }
}