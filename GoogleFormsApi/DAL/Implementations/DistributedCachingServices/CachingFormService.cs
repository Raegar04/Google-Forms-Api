using Application.Abstractions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Implementations.DistributedCachingServices
{
    public class CachingFormService : IFormService
    {
        private readonly TimeSpan _duration;

        private readonly IFormService _decorated;

        private readonly IDistributedCache _distributedCache;

        public CachingFormService(IFormService formService, IDistributedCache distributedCache)
        {
            _decorated = formService;
            _distributedCache = distributedCache;
            _duration = TimeSpan.FromMinutes(5);
        }

        public async Task<Form?> GetByIdAsync(Guid id)
        {
            var cached = await _distributedCache.GetStringAsync(CachingConstants.FormById(id));
            if (string.IsNullOrEmpty(cached))
            {
                var item = await _decorated.GetByIdAsync(id);
                if (item != null)
                {
                    var serializedData = JsonSerializer.Serialize(item);
                    await _distributedCache.SetStringAsync(CachingConstants.FormById(id), serializedData, new DistributedCacheEntryOptions
                    {
                        SlidingExpiration = _duration
                    });
                }

                return item;
            }

            return JsonSerializer.Deserialize<Form>(cached);
        }

        public async Task<IEnumerable<Form>> GetByHolder(Guid holderId)
        {
            throw new NotImplementedException();
            //return await _memoryCache.GetOrCreateAsync(CachingConstants.FormsByHolderId(holderId), async entry =>
            //{
            //    entry.SlidingExpiration = _duration;
            //    return await _decorated.GetByHolder(holderId);
            //});
        }

        public async Task AddAsync(Form entity)
        {
            await _decorated.AddAsync(entity);
            InvalidateCache(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var form = await _decorated.GetByIdAsync(id);
            await _decorated.DeleteAsync(id);
            InvalidateCache(form);
        }

        public async Task UpdateAsync(Form entity)
        {
            await _decorated.UpdateAsync(entity);
            InvalidateCache(entity);
        }

        public async Task<IEnumerable<Form>> GetAllAsync(Expression<Func<Form, bool>>? predicate)
        {
            return await _decorated.GetAllAsync(predicate);
        }

        private void InvalidateCache(Form form)
        {
            //_memoryCache.Remove(CachingConstants.FormsByHolderId(form.HolderId));
            //_memoryCache.Remove(CachingConstants.FormById(form.Id));
        }
    }
}
