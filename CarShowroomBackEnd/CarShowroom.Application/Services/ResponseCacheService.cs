using CarShowroom.Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace CarShowroom.Application.Services
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDistributedCache _cache;

        public ResponseCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }
        public async Task CacheResponseAsync(string cacheKey, string response, TimeSpan timeToLive)
        {
            if (response == null)
                return;

            await _cache.SetStringAsync(cacheKey, response, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeToLive
            });
        }

        public async Task<string> GetCachedResponseAsync(string cacheKey)
        {
            var cachedResponse = await _cache.GetStringAsync(cacheKey);

            return String.IsNullOrEmpty(cachedResponse) ? null : cachedResponse;
        }
    }
}
