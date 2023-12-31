﻿namespace TiktokAPI.Core.Interfaces
{
    public interface IUnitOfWork<out TContext> : IUnitOfWork where TContext : class
    {
        /// <summary>
        /// Gets the database context
        /// </summary>
        /// <returns>The instance of type <typeparamref name="TContext"/>.</returns> 
        TContext DbContext { get; }
        /// <summary>
        /// Save all changes made in this context to the database with disrtibuted transaction
        /// </summary>
        /// <param name="unitOfWorks">An optional <see cref="IUnitOfWork"/> array.</param>
        /// <returns>A <see cref="Task"/>that represents the asynchronous save operation. The task result contains the number of state entities written to database</returns>
        Task<int> SaveChangesAsync(bool ensureAutoHistory = false, params IUnitOfWork[] unitOfWorks);
    }
}
