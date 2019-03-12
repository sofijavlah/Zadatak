using System.Collections.Generic;

namespace Zadatak.Interfaces
{
    /// <summary>
    /// Generic repository
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>
        /// List of all entities.
        /// </returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Entity with given Id.
        /// </returns>
        TEntity Get(long id);

        /// <summary>
        /// Adds the specified example.
        /// </summary>
        /// <param name="example">The example.</param>
        void Add(TEntity example);

        /// <summary>
        /// Updates the specified example.
        /// </summary>
        /// <param name="example">The example.</param>
        void Update(TEntity example);

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Remove(TEntity entity);
    }
}
