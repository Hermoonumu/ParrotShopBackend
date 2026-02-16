
using System.Reflection;
using ParrotShopBackend.Application.DTO;
using ParrotShopBackend.Domain;
using StackExchange.Redis;

namespace ParrotShopBackend.Application.Extensions;


public class RedisCacheExtension
{
    private IConnectionMultiplexer _muxer;
    public IDatabase _redis;
    private IServer _srv;
    public RedisCacheExtension(IConnectionMultiplexer muxer)
    {
        _muxer = muxer;
        _redis = _muxer.GetDatabase();
        _srv = _muxer.GetServer(_muxer.GetEndPoints().FirstOrDefault()!);
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
        RedisKey[] keys = PrepareSetKeys(pfDTO);
        HashSet<long> FilterRes=[];
        HashSet<long> ByPrice = [];
        if (keys.Length > 0)
        {
            RedisValue[] FilteredByTrait = await _redis.SetCombineAsync(SetOperation.Intersect, keys);
            if (FilteredByTrait.Length == 0) return [];
            FilterRes = [.. FilteredByTrait.Select(x => (long)x)];
        }
        if (pfDTO.PriceFrom.HasValue || pfDTO.PriceTo.HasValue)
        {
            RedisValue[] FilteredByPrice = await _redis.SortedSetRangeByScoreAsync("Set_Price",
                                                pfDTO.PriceFrom??double.NegativeInfinity, 
                                                pfDTO.PriceTo??double.PositiveInfinity);
            if (FilteredByPrice.Length == 0) return [];
            ByPrice = [.. FilteredByPrice.Select(x => (long)x)];
            if (FilterRes.Count() > 0) return [.. FilterRes.Intersect(ByPrice)];
        }
        if (FilterRes.Count == 0 && ByPrice.Count == 0)
        {
            return [.. (await _redis.SetMembersAsync("Set_AllParrots")).Select(x => (long)x)];
        }
        return [.. FilterRes];

    }
    public async Task DeleteOldInfoFromCacheAsync<T>(long ParrotId, T oldObj)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateCacheOnNewInfoAsync<T>(long ParrotId, T obj)
    {
        if (obj is null) return;
        var redisTasks = new List<Task>();
        var redisBatch = _redis.CreateBatch();
        
        foreach(RedisKey key in PrepareSetKeys(obj))
        {
            redisTasks.Add(redisBatch.SetAddAsync(key, ParrotId));
        }
        redisBatch.Execute();
        await Task.WhenAll(redisTasks);
    }

    public RedisKey[] PrepareSetKeys<T>(T obj)
    {
        if (obj is null) return Array.Empty<RedisKey>();
        PropertyInfo[] PList = obj.GetType().GetProperties();
        List<string> RedisSets = [];
        foreach (PropertyInfo pi in PList)
        {
            if (pi.Name == "PriceFrom" || pi.Name == "PriceTo" || pi.Name == "AscendingPrice")
                continue;
            if (pi.GetValue(obj) is null) continue;
            if (pi.Name == "Color")
            {
                foreach (Color col in pi.GetValue(obj) as List<Color>)
                {
                    RedisSets.Add($"Set_{pi.Name}_{col}");
                }
                continue;
            }
            RedisSets.Add($"Set_{pi.Name}_{pi.GetValue(obj)}");
        }
        return RedisSets.Select(x => (RedisKey)x).ToArray();
    }

}