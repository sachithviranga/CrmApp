namespace CrmApp.Shared.DTO
{
    /// <summary>
    /// Customer data transfer object used for API responses and client consumption.
    /// Contains denormalized fields like FullName and formatted date strings.
    /// </summary>
    public class CustomerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string CreatedDate { get; set; } = string.Empty;
        public string UpdatedDate { get; set; } = string.Empty;
    }
}
