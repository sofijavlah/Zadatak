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
        /// <param name="disable"></param>
        void Start(bool disable);

        /// <summary>
        /// Gets the read only.
        /// </summary>
        /// <returns></returns>
        bool GetReadOnly();

        /// <summary>
        /// Commits this instance.
        /// </summary>
        void Commit();
    }
}
