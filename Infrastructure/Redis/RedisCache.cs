using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.Redis;

public class RedisCache(IDistributedCache cache) : IRedisCache
{
    
}