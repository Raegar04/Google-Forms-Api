using Application.Abstractions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Implementations.CachingServices
{
    public class CachingFormService : IFormService
    {
        private readonly TimeSpan _duration;

        private readonly IFormService _formService;

        private readonly IMemoryCache _memoryCache;

        public CachingFormService(IFormService formService, IMemoryCache cache)
        {
            _formService = formService;
            _memoryCache = cache;
            _duration = TimeSpan.FromMinutes(5);
        }

        public async Task<Form?> GetByIdAsync(Guid id)
        {
            return await _memoryCache.GetOrCreateAsync(CachingConstants.FormById(id), async entry =>
            {
                entry.SlidingExpiration = _duration;
                return await _formService.GetByIdAsync(id);
            });
        }

        public async Task<IEnumerable<Form>> GetByHolder(Guid holderId)
        {
            return await _memoryCache.GetOrCreateAsync(CachingConstants.FormsByHolderId(holderId), async entry =>
            {
                entry.SlidingExpiration = _duration;
                return await _formService.GetByHolder(holderId);
            });
        }

        public async Task AddAsync(Form entity)
        {
            await _formService.AddAsync(entity);
            InvalidateCache(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var form = await _formService.GetByIdAsync(id);
            await _formService.DeleteAsync(id);
            InvalidateCache(form);
        }

        public async Task UpdateAsync(Form entity)
        {
            await _formService.UpdateAsync(entity);
            InvalidateCache(entity);
        }

        public async Task<IEnumerable<Form>> GetAllAsync(Expression<Func<Form, bool>>? predicate)
        {
            return await _formService.GetAllAsync(predicate);
        }

        private void InvalidateCache(Form form)
        {
            _memoryCache.Remove(CachingConstants.FormsByHolderId(form.HolderId));
            _memoryCache.Remove(CachingConstants.FormById(form.Id));
        }
    }
}
