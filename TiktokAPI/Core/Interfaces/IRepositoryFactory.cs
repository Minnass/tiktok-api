using TiktokAPI.Core.Implementations;

namespace TiktokAPI.Core.Interfaces
{
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Get the specified repository for the <typeparamref name="TEntity"/>
        /// </summary>
        /// <typeparam name="TEntity">The type of entity</typeparam>
        /// <returns>An instance of type inherited from <see cref="IRepository{TEntity}"/>interfaces</returns>
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        /// <summary>
        /// Get the specified repository for the <typeparamref name="TEntity"/>
        /// </summary>
        /// <typeparam name="TEntity">The type of entity</typeparam>
        /// <returns>An instance of type inherited from <see cref="IRepository{TEntity}"/>interfaces</returns>
        IReadOnlyRepository<TEntity> GetReadOnlyRepository<TEntity>() where TEntity : class;
    }
}
