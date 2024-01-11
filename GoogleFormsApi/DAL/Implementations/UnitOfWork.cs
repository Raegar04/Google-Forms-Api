using BLL.Abstractions;
using Core;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    /// <summary>
    /// Unit of work implementation
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GoogleFormsDbContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Db context injected object</param>
        public UnitOfWork(GoogleFormsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get repository for entity
        /// </summary>
        /// <typeparam name="TEntity">Specified entity</typeparam>
        /// <returns>Repository for entity</returns>
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new Repository<TEntity>(_context);
        }

        private bool disposed = false;

        /// <summary>
        /// Manual disposing DvdRentalContext object.
        /// </summary>
        /// <param name="disposing">Specifies if method is runned manually</param>
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposed = true;
            }
        }

        /// <summary>
        /// IDisposable implementation with suppressing finalize if resources already have been disposed.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
