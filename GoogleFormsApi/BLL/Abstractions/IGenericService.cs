using BLL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IGenericService<TEntity> where TEntity : class
    {
        public Task AddAsync(TEntity entity);

        public Task DeleteAsync(Guid id);

        public Task UpdateAsync(TEntity entity);

        public Task<TEntity?> GetByIdAsync(Guid id);

        public Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate);
    }
}
