using System.Diagnostics.CodeAnalysis;

namespace TiktokAPI.Core.Interfaces
{
    
    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
    {
        #region Create
        /// <summary>
        /// Inserts a new entity synchronously
        /// </summary>
        /// <param name="entity"></param>
        void Insert([NotNull] TEntity entity);
        /// <summary>
        /// Insert a range of entities synchronously
        /// </summary>
        /// <param name="entities"></param>
        void Insert([NotNull] IEnumerable<TEntity> entities);
        /// <summary>
        /// Inserts a new entity asynchronously
        /// </summary>
        /// <param name="entity">The entity to insert</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete</param>
        /// <returns>A <see cref="Task"/>that represents the asynchronous insert operation</returns>
        ValueTask<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken);
        /// <summary>
        /// Inserts a new entity asynchronously
        /// </summary>
        /// <param name="entity">The entity to insert</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete</param>
        /// <returns>A <see cref="Task"/>that represents the asynchronous insert operation</returns>
        Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
        #endregion

        #region Update
        /// <summary>
        /// Updates the specified entity
        /// </summary>
        /// <param name="entity">The entity</param>
        void Update([NotNull] TEntity entity);
        /// <summary>
        /// Updates the specified entities
        /// </summary>
        /// <param name="entities">The entities</param>
        void Update([NotNull] params TEntity[] entities);
        /// <summary>
        /// Updates the specified entities
        /// </summary>
        /// <param name="entities">The entities</param>
        void Update([NotNull] IEnumerable<TEntity> entities);

        #endregion

        #region Delete
        /// <summary>
        /// Delete the entity by the specified primary key
        /// </summary>
        /// <param name="keyValues">The primary key value</param>
        void Delete([MaybeNull] params object[] keyValues);
        /// <summary>
        /// Delete the specified entity
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        void Delete([NotNull] TEntity entity);
        /// <summary>
        /// Delete the specified entities
        /// </summary>
        /// <param name="entities">The entities</param>

        void Delete([NotNull] IEnumerable<TEntity> entities);

        #endregion
        IQueryable<TEntity> Queryable();
    }
}
