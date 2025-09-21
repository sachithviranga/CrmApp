using CrmApp.Shared.DTO;

namespace CrmApp.Client.Services
{
    /// <summary>
    /// Provides methods to interact with the Customer API endpoints.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CustomerApiClient"/> class.
    /// </remarks>
    /// <param name="http">Injected HttpClient for API calls.</param>
    public class CustomerApiClient(HttpClient http)
    {
        private readonly HttpClient _http = http;

        /// <summary>
        /// Retrieves all customers.
        /// </summary>
        /// <returns>List of CustomerDto objects.</returns>
        public async Task<List<CustomerDto>> GetCustomersAsync()
        {
            return await _http.GetFromJsonAsync<List<CustomerDto>>("api/v1/customer")
                   ?? new List<CustomerDto>();
        }

        /// <summary>
        /// Retrieves a single customer by ID.
        /// </summary>
        /// <param name="id">Customer ID.</param>
        /// <returns>CustomerDto if found; otherwise, null.</returns>
        public async Task<CustomerDto?> GetCustomerAsync(int id)
        {
            return await _http.GetFromJsonAsync<CustomerDto>($"api/v1/customer/{id}");
        }

        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="dto">Customer creation request data.</param>
        /// <returns>ErrorResponse if creation fails; otherwise, null.</returns>
        public async Task<ErrorResponse?> CreateCustomerAsync(CreateCustomerRequest dto)
        {
            var resp = await _http.PostAsJsonAsync("api/v1/customer", dto);
            if (resp.IsSuccessStatusCode) return null;
            return await resp.Content.ReadFromJsonAsync<ErrorResponse>();
        }

        /// <summary>
        /// Updates an existing customer.
        /// </summary>
        /// <param name="dto">Customer update request data.</param>
        /// <returns>ErrorResponse if update fails; otherwise, null.</returns>
        public async Task<ErrorResponse?> UpdateCustomerAsync(UpdateCustomerRequest dto)
        {
            var resp = await _http.PutAsJsonAsync($"api/v1/customer", dto);
            if (resp.IsSuccessStatusCode) return null;
            return await resp.Content.ReadFromJsonAsync<ErrorResponse>();
        }

        /// <summary>
        /// Deletes a customer by ID.
        /// </summary>
        /// <param name="id">Customer ID.</param>
        /// <returns>ErrorResponse if deletion fails; otherwise, null.</returns>
        public async Task<ErrorResponse?> DeleteCustomerAsync(int id)
        {
            var resp = await _http.DeleteAsync($"api/v1/customer/{id}");
            if (resp.IsSuccessStatusCode) return null;
            return await resp.Content.ReadFromJsonAsync<ErrorResponse>();
        }

        /// <summary>
        /// Retrieves a paged list of customers.
        /// </summary>
        /// <param name="page">Page number.</param>
        /// <param name="pageSize">Number of items per page.</param>
        /// <returns>PagedResult containing CustomerDto objects.</returns>
        public async Task<PagedResult<CustomerDto>> GetCustomersPagedAsync(int page, int pageSize)
        {
            return await _http.GetFromJsonAsync<PagedResult<CustomerDto>>($"api/v1/customer/paged?page={page}&pageSize={pageSize}")
                   ?? new PagedResult<CustomerDto>();
        }
    }
}
