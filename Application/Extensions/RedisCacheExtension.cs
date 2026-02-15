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
        await _redis.StringSetAsync(key, value);
        if (ttl is not null) _redis.KeyExpire(key, ttl);
    }

    public async Task<string?> GetStringAsync(string key)
    {
        return await _redis.StringGetAsync(key);
    }
    

}