using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using TiktokAPI.Core.Interfaces;

namespace TiktokAPI.Core.Implementations
{
    public class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// The database context
        /// </summary>
        protected readonly DbContext DbContext;
        /// <summary>
        /// The database set of <see cref="TEntity"/>
        /// </summary>

        protected readonly DbSet<TEntity> DbSet;
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context</param>

        public ReadOnlyRepository([NotNull] DbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<TEntity>();
        }

        #region Read
        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = DbSet;
            if (disableTracking)
                query = query.AsNoTracking();
            if (include != null)
                query = include(query);
            if (predicate != null)
                query.Where(predicate);
            return orderBy != null ? orderBy(query) : query;
        }

        public virtual TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = DbSet;
            if (disableTracking)
                query = query.AsNoTracking();
            if (include != null)
                query = include(query);
            if (predicate != null)
                query.Where(predicate);
            return orderBy != null ? orderBy(query).FirstOrDefault() : query.FirstOrDefault();
        }

        public virtual TResult GetFirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TResult>,
                IOrderedQueryable<TResult>> orderBy = null, Func<IQueryable<TEntity>,
                    IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true) where TResult : class
        {
            IQueryable<TEntity> query = DbSet;
            if (disableTracking)
                query = query.AsNoTracking();
            if (include != null)
                query = include(query);
            if (predicate != null)
                query.Where(predicate);
            IQueryable<TResult> queryResult = query.Select(selector);
            return orderBy != null ? orderBy(queryResult).FirstOrDefault() : queryResult.FirstOrDefault();
        }

        public virtual Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = DbSet;
            if (disableTracking)
                query = query.AsNoTracking();
            if (include != null)
                query = include(query);
            if (predicate != null)
                query.Where(predicate);
            return orderBy != null ? orderBy(query).AsQueryable().FirstOrDefaultAsync() : query.AsQueryable().FirstOrDefaultAsync();
        }

        public virtual Task<TResult> GetFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TResult>,
                IOrderedQueryable<TResult>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity,
                    object>> include = null, bool disableTracking = true) where TResult : class
        {
            IQueryable<TEntity> query = DbSet;
            if (disableTracking)
                query = query.AsNoTracking();
            if (include != null)
                query = include(query);
            if (predicate != null)
                query.Where(predicate);
            IQueryable<TResult> queryResult = query.Select(selector);
            return orderBy != null ? orderBy(queryResult).AsQueryable().FirstOrDefaultAsync() : queryResult.AsQueryable().FirstOrDefaultAsync();
        }

        public virtual IQueryable<TEntity> FromSqlRaw(string sql, params object[] parameters)
        {
            return DbSet.FromSqlRaw(sql, parameters);
        }

        public virtual IQueryable<TEntity> FromSqlInterpolated(FormattableString sql)
        {
            return DbSet.FromSqlInterpolated(sql);
        }

        public virtual TEntity Find([MaybeNull] params object[] keyValues)
        {
            return DbSet.Find(keyValues);
        }

        public virtual ValueTask<TEntity> FindAsync([MaybeNull] params object[] keyValues)
        {
            return DbSet.FindAsync(keyValues);
        }

        public virtual ValueTask<TEntity> FindAsync([MaybeNull] object[] keyValues, CancellationToken cancellationToken)
        {
            return DbSet.FindAsync(keyValues, cancellationToken);
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate != null ? DbSet.Count(predicate) : DbSet.Count();
        }

        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null, CancellationToken cancellationToken = default)
        {
            return predicate != null ? DbSet.CountAsync(predicate, cancellationToken) : DbSet.CountAsync(cancellationToken);
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate != null ? DbSet.Any(predicate) : DbSet.Any();
        }

        #endregion


       
    }
}
