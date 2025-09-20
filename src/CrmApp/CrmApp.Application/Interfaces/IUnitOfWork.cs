using Microsoft.EntityFrameworkCore.Storage;

namespace CrmApp.Application.Interfaces
{
    /// <summary>
    /// Unit of Work pattern for managing database transactions
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Begins a new transaction
        /// </summary>
        /// <returns>Database transaction</returns>
        Task<IDbContextTransaction> BeginTransactionAsync();

        /// <summary>
        /// Commits the current transaction
        /// </summary>
        Task CommitAsync();

        /// <summary>
        /// Rolls back the current transaction
        /// </summary>
        Task RollbackAsync();

        /// <summary>
        /// Saves changes to the database
        /// </summary>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Gets the customer repository
        /// </summary>
        ICustomerRepository CustomerRepository { get; }
    }
}
