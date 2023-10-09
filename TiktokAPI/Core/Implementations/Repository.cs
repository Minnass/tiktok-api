using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using TiktokAPI.Core.Interfaces;

namespace TiktokAPI.Core.Implementations
{
    public class Repository<TEntity>:ReadOnlyRepository<TEntity>,IRepository<TEntity> where TEntity:class
    {
        public Repository([NotNull] DbContext dbContext) : base(dbContext)
        { }

        #region Create
        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        public virtual async ValueTask<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken)
        {
            return (await DbSet.AddAsync(entity, cancellationToken)).Entity;
        }

        public virtual Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            return DbSet.AddRangeAsync(entities, cancellationToken);
        }
        #endregion

        #region Update
        public virtual void Update([NotNull] TEntity entity)
        {
            DbSet.Update(entity);
        }
        public virtual void Update([NotNull] params TEntity[] entities)
        {
            DbSet.UpdateRange(entities);
        }
        public virtual void Update([NotNull] IEnumerable<TEntity> entities)
        {
            DbSet.UpdateRange(entities);
        }

        #endregion

        #region Delete

        public virtual void Delete([NotNull] params object[] keyValues)
        {
            TEntity entity = DbSet.Find(keyValues);
            if (entity != null) Delete(entity);
        }
        public virtual void Delete([NotNull] TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public virtual void Delete([NotNull] IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
        }
        #endregion
     
        public IQueryable<TEntity> Queryable()
        {
            return DbSet;
        }

    }
}
