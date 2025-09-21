using CrmApp.Shared.DTO;

namespace CrmApp.Client.Services
{
    public class CustomerApiClient
    {
        private readonly HttpClient _http;

        public CustomerApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<CustomerDto>> GetCustomersAsync()
        {
            return await _http.GetFromJsonAsync<List<CustomerDto>>("api/v1/customer")
                   ?? new List<CustomerDto>();
        }

        public async Task<CustomerDto?> GetCustomerAsync(int id)
        {
            return await _http.GetFromJsonAsync<CustomerDto>($"api/v1/customer/{id}");
        }

        public async Task<ErrorResponse?> CreateCustomerAsync(CreateCustomerRequest dto)
        {
            var resp = await _http.PostAsJsonAsync("api/v1/customer", dto);
            if (resp.IsSuccessStatusCode) return null;
            return await resp.Content.ReadFromJsonAsync<ErrorResponse>();
        }

        public async Task<ErrorResponse?> UpdateCustomerAsync(UpdateCustomerRequest dto)
        {
            var resp = await _http.PutAsJsonAsync($"api/v1/customer", dto);
            if (resp.IsSuccessStatusCode) return null;
            return await resp.Content.ReadFromJsonAsync<ErrorResponse>();
        }

        public async Task<ErrorResponse?> DeleteCustomerAsync(int id)
        {
            var resp = await _http.DeleteAsync($"api/v1/customer/{id}");
            if (resp.IsSuccessStatusCode) return null;
            return await resp.Content.ReadFromJsonAsync<ErrorResponse>();
        }
    }
}
