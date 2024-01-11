using BLL.Abstractions;
using BLL.Helpers;
using Core;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    /// <summary>
    /// Generic repository interface
    /// </summary>
    /// <typeparam name="TEntity">Managing entity type</typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly GoogleFormsDbContext _context;

        private readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Db context injected object</param>
        public Repository(GoogleFormsDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="entity">Entity to add</param>
        /// <returns>Indicating success and error message or result data</returns>
        public async Task<Result<bool>> AddAsync(TEntity entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
                return new Result<bool>(true);
            }
            catch
            {
                return new Result<bool>(false, "Cannot add item");
            }
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="id">Entity's to delete id</param>
        /// <returns>Indicating success and error message or result data</returns>
        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var entityToDelete = await _dbSet.FindAsync(id);
            if (entityToDelete == null)
            {
                return new Result<bool>(false, "Cannot find item");
            }

            _dbSet.Remove(entityToDelete);
            await _context.SaveChangesAsync();
            return new Result<bool>(true);
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="compositePkValues">Entity's composite primary key values to find it</param>
        /// <returns>Indicating success and error message or result data</returns>
        public async Task<Result<bool>> DeleteAsync(object[] compositePkValues)
        {
            var entityToDelete = await _dbSet.FindAsync(compositePkValues);
            if (entityToDelete == null)
            {
                return new Result<bool>(false, "Cannot find item");
            }

            _dbSet.Remove(entityToDelete);
            await _context.SaveChangesAsync();
            return new Result<bool>(true);
        }

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>Indicating success and error message or result data</returns>
        public async Task<Result<IEnumerable<TEntity>>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            var query = _dbSet.AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return new Result<IEnumerable<TEntity>>(true, await query.ToListAsync());
        }

        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id">Searched entity's id</param>
        /// <returns>Indicating success and error message or result data</returns>
        public async Task<Result<TEntity>> GetByIdAsync(int id)
        {
            var result = await _dbSet.FindAsync(id);
            if (result is null)
            {
                return new Result<TEntity>(false, "Cannot find item");
            }

            return new Result<TEntity>(true, result);
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="id">Searched entity's id</param>
        /// <param name="entity">Entity to update</param>
        /// <returns>Indicating success and error message or result data</returns>
        public async Task<Result<bool>> UpdateAsync(int id, TEntity entity)
        {
            try
            {
                var existing = _dbSet.Find(id);
                if (existing == null)
                {
                    return new Result<bool>(false, "Cannot find item");
                }

                _context.Entry(existing).State = EntityState.Detached;
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return new Result<bool>(true);
            }
            catch
            {
                return new Result<bool>(false, "Cannot update item");
            }
        }
    }
}
