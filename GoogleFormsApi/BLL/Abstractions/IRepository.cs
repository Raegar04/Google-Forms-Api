using BLL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstractions
{
    /// <summary>
    /// Generic repository interface
    /// </summary>
    /// <typeparam name="TEntity">Managing entity type</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>Indicating success and error message or result data</returns>
        Task<Result<IEnumerable<TEntity>>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null);

        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id">Searched entity's id</param>
        /// <returns>Indicating success and error message or result data</returns>
        Task<Result<TEntity>> GetByIdAsync(Guid id);

        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="entity">Entity to add</param>
        /// <returns>Indicating success and error message or result data</returns>
        Task<Result<bool>> AddAsync(TEntity entity);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="id">Searched entity's id</param>
        /// <param name="entity">Entity to update</param>
        /// <returns>Indicating success and error message or result data</returns>
        Task<Result<bool>> UpdateAsync(Guid id, TEntity entity);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="id">Entity's to delete id</param>
        /// <returns>Indicating success and error message or result data</returns>
        Task<Result<bool>> DeleteAsync(Guid id);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="compositePkValues">Entity's composite primary key values to find it</param>
        /// <returns>Indicating success and error message or result data</returns>
        Task<Result<bool>> DeleteAsync(object[] compositePkValues);

        Task<Result<bool>> AddRangeAsync(IEnumerable<TEntity> entities);
    }
}
