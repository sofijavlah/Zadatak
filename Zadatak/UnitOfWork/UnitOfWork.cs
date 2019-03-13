using Microsoft.EntityFrameworkCore.Storage;
using Zadatak.Interfaces;
using Zadatak.Models;

namespace Zadatak.UnitOfWork
{
    /// <summary>
    /// Unit of Work
    /// </summary>
    /// <seealso cref="Zadatak.Interfaces.IUnitOfWork" />
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WorkContext _context;

        private IDbContextTransaction _transaction;

        //private OfficeRepository _officeRepository;

        //private EmployeeRepository _employeeRepository;

        //private DeviceRepository _deviceRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UnitOfWork(WorkContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
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
