using Microsoft.EntityFrameworkCore.Query;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;


namespace TiktokAPI.Core.Interfaces
{
  
    public interface IReadOnlyRepository<TEntity> where TEntity : class
    {
        #region Read
        /// <summary>
        /// Get all entities
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <param name="orderBy">A function to order elements</param>
        /// <param name="include">A function to include navigation properties</param>
        /// <param name="disalbeTracking"><c>True</c> to disable changing tracking; otherwise,<c>false</c>.Default to<c>True</c></param>
        /// <returns>The <see cref="IQueryable"/>.</returns>
        /// <exception cref="InvalidOperationException">If <see cref="TEntity"/> DbSet does not exist in DbContext</exception>
        IQueryable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>,
                IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true);
        /// <summary>
        /// Get the first or default entity based on a predicate, order by delegate and include delegate
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <param name="orderBy">A function to order elements</param>
        /// <param name="include">A function to include navigation properties</param>
        /// <param name="disalbeTracking"><c>True</c> to disable changing tracking; otherwise,<c>false</c>.Default to<c>True</c></param>
        /// <returns></returns>
        TEntity GetFirstOrDefault(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true);
        /// <summary>
        /// Get the first or default entity based on a predicate, order by delegate and include delegate
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector">The selector for projection</param>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <param name="orderBy">A function to order elements</param>
        /// <param name="include">A function to include navigation properties</param>
        /// <param name="disalbeTracking"><c>True</c> to disable changing tracking; otherwise,<c>false</c>.Default to<c>True</c></param>
        /// <remarks>This method defaults to a read-only.no tracking query.</remarks>
        TResult GetFirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TResult>, IOrderedQueryable<TResult>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true) where TResult : class;
        /// <summary>
        /// Get the first or default entity based on a predicate, order by delegate and include delegate
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <param name="orderBy">A function to order elements</param>
        /// <param name="include">A function to include navigation properties</param>
        /// <param name="disalbeTracking"><c>True</c> to disable changing tracking; otherwise,<c>false</c>.Default to<c>True</c></param>
        ///<remarks>Ex: This method defaults to a read-only, no-tracking query.</remarks>
        Task<TEntity> GetFirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate = null,
                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true);
        /// <summary>
        /// Get the first or default entity based on a predicate, order by delegate and include delegate
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector">The selector for projection</param>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <param name="orderBy">A function to order elements</param>
        /// <param name="include">A function to include navigation properties</param>
        /// <param name="disalbeTracking"><c>True</c> to disable changing tracking; otherwise,<c>false</c>.Default to<c>True</c></param>
        /// <remarks>This method defaults to a read-only.no tracking query.</remarks>
        Task<TResult> GetFirstOrDefaultAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TResult>, IOrderedQueryable<TResult>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true) where TResult : class;
        /// <summary>
        /// User raw SQl queries to fetch the specified <typeparamref name="TEntity"/>data.
        /// </summary>
        /// <param name="sql">The raw SQL</param>
        /// <param name="parameters">The parameters</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>That contains elements that satisfy the condition specified by raw SQL</returns>
        IQueryable<TEntity> FromSqlRaw(string sql, params object[] parameters);
        /// <summary>
        /// Uses interpolated SQL queries to fetch the specified <typeparamref name="TEntity"/>data.
        /// </summary>
        /// <param name="sql">The interpolated SQL</param>
        /// <returns></returns>
        IQueryable<TEntity> FromSqlInterpolated(FormattableString sql);
        /// <summary>
        /// Finds an entity with the given primary key values. If found, is attached to the context and returned.If no entity is found, the null is returned.
        /// </summary>
        /// <param name="keyValues">The values of the primary key for the entiry to be found</param>
        /// <returns>The found entity or null</returns>
        TEntity Find([MaybeNull] params object[] keyValues);
        /// <summary>
        /// Finds an entity with the given primary key values. If found, is attached to the context and returned.If no entity is found, the null is returned.
        /// </summary>
        /// <param name="keyValues">The values of the primary key for the entiry to be found</param>
        /// <returns>A <see cref="Task{TResult}"/>that represents the asynchronous find operation. The task result contains the found entity or null</returns>
        ValueTask<TEntity> FindAsync([MaybeNull] params object[] keyValues);
        /// <summary>
        /// Finds an entity with the given primary key values. If found, is attached to the context and returned.If no entity is found, the null is returned.
        /// </summary>
        /// <param name="keyValues">The values of the primary key for the entiry to be found</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/>to observe while waiting for the task to complete</param>
        /// <returns>A <see cref="Task{TResult}"/>that represents the asynchronous find operation. The task result contains the found entity or null</returns>
        ValueTask<TEntity> FindAsync([MaybeNull] object[] keyValues, CancellationToken cancellationToken);

        #endregion

        #region Other
        /// <summary>
        /// Get the count based on a predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> predicate = null);
        /// <summary>
        /// Get the count based on a predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/>to observe while waiting for the task to complete</param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null, CancellationToken cancellationToken = default);
        /// <summary>
        /// Checked exist record by predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Any(Expression<Func<TEntity, bool>> predicate = null);

        #endregion
    }
}
