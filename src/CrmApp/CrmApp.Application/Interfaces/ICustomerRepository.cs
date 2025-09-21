using CrmApp.Domain.Model;
using CrmApp.Shared.DTO;

namespace CrmApp.Application.Interfaces
{
    public interface ICustomerRepository
    {
        /// <summary>
        /// Gets a customer by ID
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns>Customer or null if not found</returns>
        Task<Customer?> GetByIdAsync(int id);

        // Add additional repository methods below as needed for customer-specific queries or operations.

        /// <summary>
        /// Retrieves all customers from the database
        /// </summary>
        /// <returns>Collection of all customers</returns>
        Task<IEnumerable<Customer>> GetAllAsync();

        /// <summary>
        /// Creates a new customer
        /// </summary>
        /// <param name="customer">Customer to create</param>
        /// <returns>Created customer</returns>
        Task<Customer> CreateAsync(Customer customer);

        /// <summary>
        /// Updates an existing customer
        /// </summary>
        /// <param name="customer">Customer to update</param>
        /// <returns>Updated customer or null if not found</returns>
        Task<Customer?> UpdateAsync(Customer customer);

        /// <summary>
        /// Deletes a customer by ID
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns>True if deleted, false if not found</returns>
        Task<bool> DeleteAsync(int id);


        /// <summary>
        /// Checks if a customer with the specified email already exists
        /// </summary>
        /// <param name="email">Email address to check</param>
        /// <param name="id">Optional customer ID to exclude from the check (for updates)</param>
        /// <returns>True if email exists, false otherwise</returns>
        Task<bool> ExistsByEmailAsync(string email, int? id);


        /// <summary>
        /// Retrieves a paged list of customers from the database
        /// </summary>
        /// <param name="page">Page number (1-based)</param>
        /// <param name="pageSize">Number of customers per page</param>
        /// <returns>Paged result containing customers and pagination info</returns>
        Task<PagedResult<Customer>> GetPagedAsync(int page, int pageSize);
    }
}
