using Application.Abstractions;
using Domain.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Implementations.DistributedCachingServices
{
    public class CachingUserFormService : IUserFormService
    {
        private readonly IUserFormService _decorated;

        private readonly IMemoryCache _cache;

        private readonly TimeSpan _duration;

        public CachingUserFormService(IUserFormService userFormService, IMemoryCache cache)
        {
            _decorated = userFormService;
            _cache = cache;
            _duration = TimeSpan.FromMinutes(5);
        }

        public async Task<UserForm?> GetByIdAsync(Guid id)
        {
            return await _cache.GetOrCreateAsync(CachingConstants.UserFormById(id), async entry =>
            {
                entry.SlidingExpiration = _duration;
                return await _decorated.GetByIdAsync(id);
            });
        }

        public async Task<IEnumerable<UserForm>> GetByForm(Guid formId)
        {
            return await _cache.GetOrCreateAsync(CachingConstants.UserFormsByFormId(formId), async entry =>
            {
                entry.SlidingExpiration = _duration;
                return await _decorated.GetByForm(formId);
            });
        }

        public async Task<IEnumerable<UserForm>> GetByUser(Guid userId)
        {
            return await _cache.GetOrCreateAsync(CachingConstants.UserFormsByUserId(userId), async entry =>
            {
                entry.SlidingExpiration = _duration;
                return await _decorated.GetByUser(userId);
            });
        }

        public async Task AddAsync(UserForm entity)
        {
            await _decorated.AddAsync(entity);
            InvalidateCache(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = await _decorated.GetByIdAsync(id);
            await _decorated.DeleteAsync(id);
            InvalidateCache(item);
        }

        public async Task<IEnumerable<UserForm>> GetAllAsync(Expression<Func<UserForm, bool>>? predicate)
        {
            return await _decorated.GetAllAsync(predicate);
        }

        public async Task UpdateAsync(UserForm entity)
        {
            await _decorated.UpdateAsync(entity);
            InvalidateCache(entity);
        }

        private void InvalidateCache(UserForm userForm)
        {
            _cache.Remove(CachingConstants.UserFormById(userForm.Id));
            _cache.Remove(CachingConstants.UserFormsByFormId(userForm.FormId));
            _cache.Remove(CachingConstants.UserFormsByUserId(userForm.UserId));
        }
    }
}
