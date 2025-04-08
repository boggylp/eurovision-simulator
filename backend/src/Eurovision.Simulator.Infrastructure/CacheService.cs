using Microsoft.Extensions.Caching.Memory;

namespace Eurovision.Simulator.Infrastructure;

public interface ICacheService
{
    Task<T> Get<T>(string cacheKey, Func<Task<T>> valueFactory);
}

public sealed class CacheService(IMemoryCache cache) : ICacheService
{
    public async Task<T> Get<T>(string cacheKey, Func<Task<T>> valueFactory)
    {
        if (cache.TryGetValue(cacheKey, out var existingValue))
        {
            if (existingValue is not T value)
            {
                throw new InvalidOperationException("Wrong type of cached value.");
            }

            return value;
        }

        var cachedValue = await valueFactory();
        var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

        cache.Set(cacheKey, cachedValue, cacheEntryOptions);

        return cachedValue;
    }
}
