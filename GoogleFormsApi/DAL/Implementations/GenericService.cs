using Application.Abstractions;
using Application.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Implementations
{
    public abstract class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class
    {
        private protected readonly GoogleFormsDbContext _context;

        private protected readonly DbSet<TEntity> _dbSet;

        private readonly IHttpContextAccessor _accessor;

        public GenericService(GoogleFormsDbContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
            _dbSet = context.Set<TEntity>();
        }

        public async virtual Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async virtual Task DeleteAsync(Guid id)
        {
            var item = await _dbSet.FindAsync(id);

            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(object id)
        {
            var item = await _dbSet.FindAsync(id);

            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async virtual Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate)
        {
            var query = _dbSet.AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.ToListAsync();
        }

        public async virtual Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async virtual Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await SaveChangesAsync();
        }

        private async Task<int> SaveChangesAsync()
        {
            var id = _accessor.HttpContext?.User.GetUserIdFromPrincipal();
            ArgumentNullException.ThrowIfNull(id);

            return await _context.SaveChangesAsync((Guid)id);
        }
    }
}
