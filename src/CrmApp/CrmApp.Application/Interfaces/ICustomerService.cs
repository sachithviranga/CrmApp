using CrmApp.Shared.DTO;

namespace CrmApp.Application.Interfaces
{
    /// <summary>
    /// Service interface for customer-related operations.
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="request">Customer creation request.</param>
        /// <returns>Created customer DTO.</returns>
        Task<CustomerDto> CreateCustomerAsync(CreateCustomerRequest request);

        /// <summary>
        /// Gets a customer by ID.
        /// </summary>
        /// <param name="id">Customer ID.</param>
        /// <returns>Customer DTO or null if not found.</returns>
        Task<CustomerDto?> GetCustomerByIdAsync(int id);

        /// <summary>
        /// Updates an existing customer.
        /// </summary>
        /// <param name="request">Customer update request.</param>
        /// <returns>Updated customer DTO or null if not found.</returns>
        Task<CustomerDto?> UpdateCustomerAsync(UpdateCustomerRequest request);

        /// <summary>
        /// Gets all customers.
        /// </summary>
        /// <returns>Collection of customer DTOs.</returns>
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();

        /// <summary>
        /// Deletes a customer by ID.
        /// </summary>
        /// <param name="id">Customer ID.</param>
        /// <returns>True if deleted, false if not found.</returns>
        Task<bool> DeleteCustomerAsync(int id);

        /// <summary>
        /// Gets a paged list of customers.
        /// </summary>
        /// <param name="page">Page number (starting from 1).</param>
        /// <param name="pageSize">Number of items per page.</param>
        /// <returns>Paged result containing customer DTOs.</returns>
        Task<PagedResult<CustomerDto>> GetPagedAsync(int page, int pageSize);
    }
}