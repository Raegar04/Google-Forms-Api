using Application.Abstractions;
using Domain.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace Persistence.Implementations.CachingServices
{
    public class CachingQuestionService : IQuestionService
    {
        private readonly IQuestionService _decorated;

        private readonly IMemoryCache _memoryCache;

        private readonly TimeSpan _duration;

        public CachingQuestionService(IQuestionService questionService, IMemoryCache cache)
        {
            _decorated = questionService;
            _memoryCache = cache;
            _duration = TimeSpan.FromMinutes(5);
        }

        public async Task<Question?> GetByIdAsync(Guid id)
        {
            return await _memoryCache.GetOrCreateAsync(CachingConstants.QuestionById(id), async entry =>
            {
                entry.SlidingExpiration = _duration;
                return await _decorated.GetByIdAsync(id);
            });
        }

        public async Task<IEnumerable<Question>> GetByForm(Guid formId)
        {
            return await _memoryCache.GetOrCreateAsync(CachingConstants.QuestionsByFormId(formId), async entry =>
            {
                entry.SlidingExpiration = _duration;
                return await _decorated.GetByForm(formId);
            });
        }

        public async Task AddAsync(Question entity)
        {
            await _decorated.AddAsync(entity);
            InvalidateCache(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var question = await _decorated.GetByIdAsync(id);
            await _decorated.DeleteAsync(id);
            InvalidateCache(question);
        }

        public async Task UpdateAsync(Question entity)
        {
            await _decorated.UpdateAsync(entity);
            InvalidateCache(entity);
        }

        public async Task<IEnumerable<Question>> GetAllAsync(Expression<Func<Question, bool>>? predicate)
        {
            return await _decorated.GetAllAsync(predicate);
        } 
        
        private void InvalidateCache(Question question) 
        {
            _memoryCache.Remove(CachingConstants.QuestionsByFormId(question.FormId));
            _memoryCache.Remove(CachingConstants.QuestionById(question.Id));
        }
    }
}
