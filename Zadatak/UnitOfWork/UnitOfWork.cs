using Microsoft.EntityFrameworkCore.Storage;
using Zadatak.Attributes;
using Zadatak.Interfaces;
using Zadatak.Models;

namespace Zadatak.UnitOfWork
{
    /// <summary>
    /// Unit of Work
    /// </summary>
    /// <seealso cref="Zadatak.Interfaces.IUnitOfWork" />
    [DefineScopeType]
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WorkContext _context;

        private bool _isReadOnly;
        private IDbContextTransaction _transaction;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UnitOfWork(WorkContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the read only.
        /// </summary>
        /// <returns></returns>
        public bool GetReadOnly()
        {
            return _isReadOnly;
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        /// <param name="disable"></param>
        public void Start(bool disable)
        {
            _isReadOnly = disable;
            if (_isReadOnly) return; 
            _transaction = _context.Database.BeginTransaction();
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Commits this instance.
        /// </summary>
        public void Commit()
        {
            if (_isReadOnly) return;
            _transaction.Commit();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _context?.Dispose();
            _transaction?.Dispose();
        }
    }
}
