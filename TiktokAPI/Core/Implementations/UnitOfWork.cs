using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Concurrent;
using System.Transactions;
using TiktokAPI.Core.Interfaces;

namespace TiktokAPI.Core.Implementations
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        public TContext DbContext { get; }

        private bool Disposed { get; set; }
        private object SyncRoot { get; }
        private IDictionary<Type, object> Repositories { get; }
        private IDictionary<Type, object> ReadOnlyReporsitories { get; }
        private ILogger<IUnitOfWork<TContext>> Logger { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IUnitOfWork{TContext}"/> class
        /// </summary>
        /// <param name="context">The context.</param>
        public UnitOfWork(TContext context)
        {
            DbContext = context ?? throw new ArgumentNullException(nameof(context));
            Disposed = false;
            SyncRoot = new object();
            Repositories = new ConcurrentDictionary<Type, object>();
            ReadOnlyReporsitories = new ConcurrentDictionary<Type, object>();
        }

        public UnitOfWork(TContext context, ILogger<IUnitOfWork<TContext>> logger) : this(context)
        {
            Logger = logger;
        }
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            Type entityType = typeof(TEntity);
            if (Repositories.ContainsKey(entityType)) return (IRepository<TEntity>)Repositories[entityType];

            try
            {
                IRepository<TEntity> customRepo = DbContext.GetService<IRepository<TEntity>>();
                Repositories[entityType] = customRepo;
                return customRepo;
            }
            catch
            {
                Logger?.LogDebug("Can't get Repository from service provider");
            }

            Repositories[entityType] = new Repository<TEntity>(DbContext);
            return (IRepository<TEntity>)Repositories[entityType];
        }
        public IReadOnlyRepository<TEntity> GetReadOnlyRepository<TEntity>() where TEntity : class
        {
            Type entityType = typeof(TEntity);
            if (ReadOnlyReporsitories.ContainsKey(entityType)) return (IRepository<TEntity>)ReadOnlyReporsitories[entityType];

            try
            {
                IRepository<TEntity> customRepo = DbContext.GetService<IRepository<TEntity>>();
                Repositories[entityType] = customRepo;
                return customRepo;
            }
            catch
            {
                // Logger?.LogDebug("Can't get Repository from service provider");
            }

            ReadOnlyReporsitories[entityType] = new Repository<TEntity>(DbContext);
            return (IRepository<TEntity>)ReadOnlyReporsitories[entityType];
        }


        #region Execute SQL
        public int ExecuteSqlInterpolated(FormattableString sql)
        {
            return DbContext.Database.ExecuteSqlInterpolated(sql);
        }
        public async Task<int> ExecuteSqlInterpolatedAsync(FormattableString sql, CancellationToken cancellationToken = default)
        {
            return await DbContext.Database.ExecuteSqlInterpolatedAsync(sql, cancellationToken);
        }

        public int ExecuteSqlRaw(string sql, IEnumerable<object> parameters = null)
        {
            return parameters == null ?
                DbContext.Database.ExecuteSqlRaw(sql) : DbContext.Database.ExecuteSqlRaw(sql, parameters);
        }
        public async Task<int> ExecuteSqlRawAsync(string sql, IEnumerable<object> parameters = null, CancellationToken cancellationToken = default)
        {
            return parameters == null ?
               await DbContext.Database.ExecuteSqlRawAsync(sql, cancellationToken)
               : await DbContext.Database.ExecuteSqlRawAsync(sql, parameters, cancellationToken);
        }
        #endregion

        #region From SQL
        public IQueryable<TEntity> FromSqlInterpolated<TEntity>(FormattableString sql) where TEntity : class
        {
            IRepository<TEntity> repository = GetRepository<TEntity>();
            return repository.FromSqlInterpolated(sql);
        }
        public IQueryable<TEntity> FromSqlRaw<TEntity>(string sql, params object[] parameters) where TEntity : class
        {
            IRepository<TEntity> repository = GetRepository<TEntity>();
            return repository.FromSqlRaw(sql, parameters);
        }
        #endregion

        #region Save Changes
        public int SaveChanges(bool ensureAutoHistory = false)
        {
            try
            {
                //if (ensureAutoHistory)
                //    DbContext.
                return DbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Logger?.LogError(e, "Error in {0}", nameof(SaveChanges));
                throw;
            }
        }
        public async Task<int> SaveChangesAsync(bool ensureAutoHistory = false)
        {
            try
            {
                //if (ensureAutoHistory)
                    //DbContext.EnsureAutoHistory();
                return await DbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Logger?.LogError(e, "Error in {0}", nameof(SaveChanges));
                throw;
            }
        }
        public async Task<int> SaveChangesAsync(bool ensureAutoHistory = false, params IUnitOfWork[] unitOfWorks)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    int count = 0;
                    foreach (IUnitOfWork unitOfWork in unitOfWorks)
                    {
                        count += await unitOfWork.SaveChangesAsync();
                    }
                    count += await SaveChangesAsync(ensureAutoHistory);

                    ts.Complete();
                    return count;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
        #endregion


        #region Disposed
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            lock (SyncRoot)
            {
                if (!Disposed && disposing)
                {
                    Repositories.Clear();
                    ReadOnlyReporsitories.Clear();
                    DbContext.Dispose();
                }
                Disposed = true;
            }
        }

        IReadOnlyRepository<TEntity> IRepositoryFactory.GetReadOnlyRepository<TEntity>()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
