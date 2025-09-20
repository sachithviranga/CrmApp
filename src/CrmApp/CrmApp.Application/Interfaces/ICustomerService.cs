using CrmApp.Shared.DTO;

namespace CrmApp.Application.Interfaces
{
    public interface ICustomerService
    {
        /// <summary>
        /// Creates a new customer
        /// </summary>
        /// <param name="request">Customer creation request</param>
        /// <returns>Created customer DTO</returns>
        Task<CustomerDto> CreateCustomerAsync(CreateCustomerRequest request);

        /// <summary>
        /// Gets a customer by ID
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns>Customer DTO or null if not found</returns>
        Task<CustomerDto?> GetCustomerByIdAsync(int id);

        /// <summary>
        /// Updates an existing customer
        /// </summary>
        /// <param name="request">Customer update request</param>
        /// <returns>Updated customer DTO or null if not found</returns>
        Task<CustomerDto?> UpdateCustomerAsync(UpdateCustomerRequest request);

        /// <summary>
        /// Gets all customers
        /// </summary>
        /// <returns>Collection of customer DTOs</returns>
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();

        /// <summary>
        /// Deletes a customer by ID
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns>True if deleted, false if not found</returns>
        Task<bool> DeleteCustomerAsync(int id);
    }
}