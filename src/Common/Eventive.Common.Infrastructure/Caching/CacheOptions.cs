using Microsoft.Extensions.Caching.Distributed;

namespace Eventive.Common.Infrastructure.Caching;

public static class CacheOptions
{
    //default expire value
    public static DistributedCacheEntryOptions DefaultExpiration => new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
    };

    //if you want custom expiration time.just pass the time
    public static DistributedCacheEntryOptions Create(TimeSpan? expiration) =>
        expiration is not null ?
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = expiration } :
            DefaultExpiration;
}
