using Microsoft.Extensions.Caching.Memory;

namespace Todo.Application.Common;

internal static class CacheExtensions
{
    private const int CacheExpirationInMinute = 60;
    private static readonly SemaphoreSlim Semaphore = new(1, 1);

    public static async ValueTask<T?> GetWithFallbackAsync<T>(
        this IMemoryCache cache, string cacheKey, Func<CancellationToken, Task<T?>> fallback, CancellationToken cancellationToken)
    {
        var fromCache = cache.Get<T>(cacheKey);
        if (fromCache is not null)
        {
            return fromCache;
        }

        try
        {
            await Semaphore.WaitAsync(cancellationToken);

            // To prevent multiple reading data from the database
            fromCache = cache.Get<T>(cacheKey);
            if (fromCache is not null)
            {
                return fromCache;
            }

            var data = await fallback(cancellationToken);
            if (data is null)
            {
                return data;
            }

            cache.Set(cacheKey, data, TimeSpan.FromMinutes(CacheExpirationInMinute));
            return data;
        }
        finally
        {
            Semaphore.Release();
        }
    }
}
