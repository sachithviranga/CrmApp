using AutoMapper;
using CrmApp.Application.Interfaces;
using CrmApp.Domain.Model;
using CrmApp.Shared.DTO;
using Microsoft.Extensions.Logging;

namespace CrmApp.Application.Services
{
    /// <summary>
    /// Service implementation for Customer business operations.
    /// Handles application layer concerns including business logic,
    /// validation, and coordination between repository and presentation layers.
    /// </summary>
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerService> _logger;

        /// <summary>
        /// Initializes a new instance of the CustomerService class.
        /// </summary>
        /// <param name="customerRepository">Repository for data access operations</param>
        /// <param name="mapper">AutoMapper instance for object mapping</param>
        /// <param name="logger">Logger instance for operation tracking</param>
        public CustomerService(ICustomerRepository customerRepository, IMapper mapper, ILogger<CustomerService> logger)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new customer with comprehensive validation and business rules.
        /// Validates email uniqueness before creation and handles all mapping operations.
        /// </summary>
        /// <param name="request">Customer creation request containing customer data</param>
        /// <returns>Created customer DTO with generated ID and computed fields</returns>
        /// <exception cref="InvalidOperationException">Thrown when customer with same email already exists</exception>
        public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerRequest request)
        {
            try
            {
                _logger.LogInformation("Creating customer with email: {Email}", request.Email);

                // Business Rule: Validate email uniqueness at application layer
                // This provides an additional layer of validation beyond the repository
                var emailExists = await _customerRepository.ExistsByEmailAsync(request.Email, null);
                if (emailExists)
                {
                    throw new InvalidOperationException($"Customer with email {request.Email} already exists.");
                }

                // Map request DTO to domain model using AutoMapper
                // This ensures proper validation through domain model constructor
                var customer = _mapper.Map<Customer>(request);

                // Delegate to repository for data persistence
                var createdCustomer = await _customerRepository.CreateAsync(customer);

                // Map domain model to response DTO with computed fields
                var responseDto = _mapper.Map<CustomerDto>(createdCustomer);

                _logger.LogInformation("Customer created successfully with ID: {Id}", createdCustomer.Id);
                return responseDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating customer with email: {Email}", request.Email);
                throw;
            }
        }

        /// <summary>
        /// Retrieves a customer by their unique ID with comprehensive error handling.
        /// Returns null if customer is not found, allowing the caller to handle appropriately.
        /// </summary>
        /// <param name="id">Customer ID to retrieve</param>
        /// <returns>Customer DTO or null if not found</returns>
        public async Task<CustomerDto?> GetCustomerByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Retrieving customer with ID: {Id}", id);

                // Delegate to repository for data retrieval
                var customer = await _customerRepository.GetByIdAsync(id);

                if (customer == null)
                {
                    _logger.LogWarning("Customer with ID {Id} not found", id);
                    return null;
                }

                // Map domain model to response DTO with computed fields
                var responseDto = _mapper.Map<CustomerDto>(customer);
                _logger.LogInformation("Customer with ID {Id} retrieved successfully", id);

                return responseDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving customer with ID: {Id}", id);
                throw;
            }
        }

        /// <summary>
        /// Updates an existing customer with comprehensive validation and business rules.
        /// Validates email uniqueness if email is being changed and enforces business rules.
        /// </summary>
        /// <param name="request">Customer update request containing updated data</param>
        /// <returns>Updated customer DTO or null if customer not found</returns>
        /// <exception cref="InvalidOperationException">Thrown when new email already exists</exception>
        public async Task<CustomerDto?> UpdateCustomerAsync(UpdateCustomerRequest request)
        {
            try
            {
                _logger.LogInformation("Updating customer with ID: {Id}", request.Id);

                // Retrieve existing customer to ensure it exists
                var existingCustomer = await _customerRepository.GetByIdAsync(request.Id);
                if (existingCustomer == null)
                {
                    _logger.LogWarning("Customer with ID {Id} not found for update", request.Id);
                    return null;
                }

                // Business Rule: Validate email uniqueness if email is being changed
                // This prevents duplicate emails when updating customer information
                if (existingCustomer.Email != request.Email)
                {
                    var emailExists = await _customerRepository.ExistsByEmailAsync(request.Email, request.Id);
                    if (emailExists)
                    {
                        throw new InvalidOperationException($"Customer with email {request.Email} already exists.");
                    }
                }

                // Update domain model with new data using domain method
                // This ensures all business rules are enforced at the domain level
                existingCustomer.UpdateDetails(
                    request.FirstName,
                    request.LastName,
                    request.Email,
                    request.PhoneNumber,
                    request.Address,
                    request.City,
                    request.State,
                    request.Country);

                // Persist changes through repository
                var updatedCustomer = await _customerRepository.UpdateAsync(existingCustomer);

                if (updatedCustomer == null)
                {
                    _logger.LogWarning("Failed to update customer with ID: {Id}", request.Id);
                    return null;
                }

                // Map updated domain model to response DTO
                var responseDto = _mapper.Map<CustomerDto>(updatedCustomer);
                _logger.LogInformation("Customer with ID {Id} updated successfully", request.Id);

                return responseDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating customer with ID: {Id}", request.Id);
                throw;
            }
        }

        /// <summary>
        /// Retrieves all customers with comprehensive error handling and performance optimization.
        /// Returns ordered collection of customer DTOs with computed fields.
        /// </summary>
        /// <returns>Collection of all customer DTOs ordered by name</returns>
        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all customers");

                // Delegate to repository for data retrieval
                // Repository handles ordering and performance optimization
                var customers = await _customerRepository.GetAllAsync();

                // Map domain models to DTOs with computed fields
                var customerDtos = _mapper.Map<IEnumerable<CustomerDto>>(customers);

                _logger.LogInformation("Retrieved {Count} customers", customerDtos.Count());
                return customerDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all customers");
                throw;
            }
        }

        /// <summary>
        /// Deletes a customer by ID with comprehensive error handling and logging.
        /// Returns boolean indicating success or failure of the operation.
        /// </summary>
        /// <param name="id">Customer ID to delete</param>
        /// <returns>True if customer was found and deleted, false if not found</returns>
        public async Task<bool> DeleteCustomerAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting customer with ID: {Id}", id);

                // Delegate to repository for deletion operation
                var deleted = await _customerRepository.DeleteAsync(id);

                if (deleted)
                {
                    _logger.LogInformation("Customer with ID {Id} deleted successfully", id);
                }
                else
                {
                    _logger.LogWarning("Customer with ID {Id} not found for deletion", id);
                }

                return deleted;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting customer with ID: {Id}", id);
                throw;
            }
        }

    }
}
