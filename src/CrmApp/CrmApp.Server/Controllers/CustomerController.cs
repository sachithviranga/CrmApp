using CrmApp.Application.Interfaces;
using CrmApp.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CrmApp.Server.Controllers
{
    /// <summary>
    /// API Controller for Customer management operations.
    /// Provides RESTful endpoints for CRUD operations on Customer entities.
    /// Handles HTTP requests and responses, input validation, and error handling.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the CustomerController class.
    /// </remarks>
    /// <param name="customerService">Service for customer business operations</param>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomerController(ICustomerService customerService) : ControllerBase
    {
        private readonly ICustomerService _customerService = customerService;

        /// <summary>
        /// Creates a new customer in the system.
        /// Validates input data and enforces business rules before creation.
        /// </summary>
        /// <param name="request">Customer creation request containing customer data</param>
        /// <returns>Created customer DTO with generated ID and computed fields</returns>
        /// <response code="201">Customer created successfully</response>
        /// <response code="400">Invalid request data or business rule violation</response>
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomer([FromBody] CreateCustomerRequest request)
        {
            // Delegate to service layer for business logic and validation
            var createdCustomer = await _customerService.CreateCustomerAsync(request);
            
            // Return 201 Created with location header pointing to the new resource
            return CreatedAtAction(nameof(GetCustomer), new { id = createdCustomer.Id }, createdCustomer);
        }

        /// <summary>
        /// Retrieves a customer by their unique ID.
        /// Returns customer data with computed fields and formatted dates.
        /// </summary>
        /// <param name="id">Customer ID to retrieve</param>
        /// <returns>Customer DTO with all details</returns>
        /// <response code="200">Customer found and returned successfully</response>
        /// <response code="404">Customer not found</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            // Delegate to service layer for data retrieval
            var customer = await _customerService.GetCustomerByIdAsync(id);
            
            if (customer == null)
                return NotFound();
                
            return customer;
        }

        /// <summary>
        /// Retrieves all customers in the system.
        /// Returns ordered collection of customers with computed fields.
        /// </summary>
        /// <returns>Collection of all customer DTOs ordered by name</returns>
        /// <response code="200">Customers retrieved successfully</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAllCustomers()
        {
            // Delegate to service layer for data retrieval
            var customers = await _customerService.GetAllCustomersAsync();
            
            return Ok(customers);
        }

        /// <summary>
        /// Updates an existing customer in the system.
        /// Validates input data and enforces business rules before updating.
        /// </summary>
        /// <param name="request">Customer update request containing updated data</param>
        /// <returns>Updated customer DTO with computed fields</returns>
        /// <response code="200">Customer updated successfully</response>
        /// <response code="400">Invalid request data or business rule violation</response>
        /// <response code="404">Customer not found</response>
        [HttpPut]
        public async Task<ActionResult<CustomerDto>> UpdateCustomer([FromBody] UpdateCustomerRequest request)
        {
            // Delegate to service layer for business logic and validation
            var updatedCustomer = await _customerService.UpdateCustomerAsync(request);
            
            if (updatedCustomer == null)
                return NotFound();

            return Ok(updatedCustomer);
        }

        /// <summary>
        /// Deletes a customer from the system by ID.
        /// Performs permanent deletion of customer data.
        /// </summary>
        /// <param name="id">Customer ID to delete</param>
        /// <returns>No content on successful deletion</returns>
        /// <response code="204">Customer deleted successfully</response>
        /// <response code="400">Error during deletion process</response>
        /// <response code="404">Customer not found</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            // Delegate to service layer for deletion operation
            var deleted = await _customerService.DeleteCustomerAsync(id);
            
            if (!deleted)
                return NotFound();

            // Return 204 No Content for successful deletion
            return NoContent();
        }

        /// <summary>
        /// Retrieves a paged list of customers.
        /// Supports pagination via query parameters.
        /// </summary>
        /// <param name="page">Page number (starting from 1)</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <returns>Paged result containing customer DTOs</returns>
        /// <response code="200">Paged customers retrieved successfully</response>
        [HttpGet("paged")]
        public async Task<ActionResult<PagedResult<CustomerDto>>> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            // Delegate to service layer for paged data retrieval
            var result = await _customerService.GetPagedAsync(page, pageSize);
            // Return 200 OK with paged result
            return Ok(result);
        }
    }
}
