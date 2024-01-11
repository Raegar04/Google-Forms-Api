using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstractions
{
    /// <summary>
    /// Unit of work interface
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Get repository for entity
        /// </summary>
        /// <typeparam name="TEntity">Specified entity</typeparam>
        /// <returns>Repository for entity</returns>
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    }
}
