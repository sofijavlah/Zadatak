using System;

namespace Zadatak.Interfaces
{
    /// <summary>
    /// Interface for Unit of Work
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Saves this instance.
        /// </summary>
        void Save();

        /// <summary>
        /// Starts this instance.
        /// </summary>
        void Start();

        /// <summary>
        /// Commits this instance.
        /// </summary>
        void Commit();
    }
}
