using AutoMapper;
using CrmApp.Application.Interfaces;
using CrmApp.Domain.Model;
using CrmApp.Infrastructure.Data;
using CrmApp.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CrmApp.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for Customer entity operations.
    /// Handles data access layer concerns including database operations,
    /// entity mapping, and business rule enforcement.
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CrmDbContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerRepository> _logger;

        /// <summary>
        /// Initializes a new instance of the CustomerRepository class.
        /// </summary>
        /// <param name="db">Database context for data access</param>
        /// <param name="mapper">AutoMapper instance for entity mapping</param>
        /// <param name="logger">Logger instance for operation tracking</param>
        public CustomerRepository(CrmDbContext db, IMapper mapper, ILogger<CustomerRepository> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new customer in the database.
        /// Validates email uniqueness and sets appropriate timestamps.
        /// </summary>
        /// <param name="customer">Customer domain model to create</param>
        /// <returns>Created customer with generated ID</returns>
        /// <exception cref="InvalidOperationException">Thrown when customer with same email already exists</exception>
        public async Task<Customer> CreateAsync(Customer customer)
        {
            _logger.LogInformation("Creating customer with email: {Email}", customer.Email);

            // Business Rule: Ensure email uniqueness
            // Check if customer with same email already exists in database
            var existingCustomer = await _db.Customer
                .FirstOrDefaultAsync(c => c.Email == customer.Email);
            
            if (existingCustomer != null)
            {
                throw new InvalidOperationException($"Customer with email {customer.Email} already exists.");
            }

            // Map domain model to entity for database persistence
            var customerEntity = _mapper.Map<CustomerEntity>(customer);
            
            // Set audit timestamps (these should be set at the database level in production)
            customerEntity.DateCreated = DateTime.UtcNow;
            customerEntity.DateUpdated = DateTime.UtcNow;

            // Add entity to context and persist to database
            await _db.Customer.AddAsync(customerEntity);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Customer created successfully with ID: {Id}", customerEntity.Id);

            // Map entity back to domain model for return
            return _mapper.Map<Customer>(customerEntity);
        }

        /// <summary>
        /// Deletes a customer from the database by ID.
        /// Performs soft delete by removing the entity from the context.
        /// </summary>
        /// <param name="id">Customer ID to delete</param>
        /// <returns>True if customer was found and deleted, false if not found</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting customer with ID: {Id}", id);

            // Find the customer entity by ID
            var entity = await _db.Customer.FindAsync(id);
            if (entity == null)
            {
                _logger.LogWarning("Customer with ID {Id} not found for deletion", id);
                return false;
            }

            // Remove entity from context and persist deletion
            _db.Customer.Remove(entity);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Customer with ID {Id} deleted successfully", id);
            return true;
        }

        /// <summary>
        /// Retrieves all customers from the database.
        /// Uses AsNoTracking for read-only performance optimization.
        /// Results are ordered by last name, then first name for consistent ordering.
        /// </summary>
        /// <returns>Collection of all customers ordered by name</returns>
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all customers");

            // Use AsNoTracking for read-only operations to improve performance
            // Order results for consistent data presentation
            var entities = await _db.Customer
                .AsNoTracking()
                .OrderBy(c => c.LastName)
                .ThenBy(c => c.FirstName)
                .ToListAsync();

            // Map entities to domain models
            var customers = _mapper.Map<List<Customer>>(entities);

            _logger.LogInformation("Retrieved {Count} customers", customers.Count);
            return customers;
        }

        /// <summary>
        /// Retrieves a customer by their unique ID.
        /// Uses AsNoTracking for read-only performance optimization.
        /// </summary>
        /// <param name="id">Customer ID to retrieve</param>
        /// <returns>Customer domain model or null if not found</returns>
        public async Task<Customer?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Retrieving customer with ID: {Id}", id);

            // Use AsNoTracking for read-only operations to improve performance
            var entity = await _db.Customer
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (entity == null)
            {
                _logger.LogWarning("Customer with ID {Id} not found", id);
                return null;
            }

            // Map entity to domain model
            var customer = _mapper.Map<Customer>(entity);
            _logger.LogInformation("Customer with ID {Id} retrieved successfully", id);

            return customer;
        }

        /// <summary>
        /// Updates an existing customer in the database.
        /// Validates email uniqueness if email is being changed.
        /// Updates the DateUpdated timestamp automatically.
        /// </summary>
        /// <param name="customer">Customer domain model with updated data</param>
        /// <returns>Updated customer domain model or null if not found</returns>
        /// <exception cref="InvalidOperationException">Thrown when new email already exists</exception>
        public async Task<Customer?> UpdateAsync(Customer customer)
        {
            _logger.LogInformation("Updating customer with ID: {Id}", customer.Id);

            // Find existing entity by ID
            var entity = await _db.Customer.FindAsync(customer.Id);
            if (entity == null)
            {
                _logger.LogWarning("Customer with ID {Id} not found for update", customer.Id);
                return null;
            }

            // Business Rule: Validate email uniqueness if email is being changed
            if (entity.Email != customer.Email)
            {
                var emailExists = await _db.Customer
                    .AnyAsync(c => c.Email == customer.Email && c.Id != customer.Id);
                
                if (emailExists)
                {
                    throw new InvalidOperationException($"Customer with email {customer.Email} already exists.");
                }
            }

            // Map updated domain model data to existing entity
            _mapper.Map(customer, entity);
            
            // Update audit timestamp
            entity.DateUpdated = DateTime.UtcNow;

            // Persist changes to database
            await _db.SaveChangesAsync();

            _logger.LogInformation("Customer with ID {Id} updated successfully", customer.Id);

            // Map updated entity back to domain model
            return _mapper.Map<Customer>(entity);
        }

        /// <summary>
        /// Checks if a customer with the specified email already exists in the database.
        /// Optionally excludes a specific customer ID from the check (useful for updates).
        /// </summary>
        /// <param name="email">Email address to check for existence</param>
        /// <param name="id">Optional customer ID to exclude from the check (for updates)</param>
        /// <returns>True if email exists, false otherwise</returns>
        public async Task<bool> ExistsByEmailAsync(string email, int? id)
        {
            _logger.LogInformation("Checking if customer with email exists: {Email}", email);

            // Build query to check email existence
            var query = _db.Customer.AsNoTracking().Where(c => c.Email == email);
            
            // Exclude specific customer ID if provided (useful for update operations)
            if (id.HasValue)
            {
                query = query.Where(c => c.Id != id.Value);
            }

            var exists = await query.AnyAsync();
            
            _logger.LogInformation("Email {Email} exists: {Exists}", email, exists);
            return exists;
        }
    }
}
