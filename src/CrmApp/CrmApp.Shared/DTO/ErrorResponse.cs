namespace CrmApp.Shared.DTO
{
    /// <summary>
    /// Standard error contract returned by the API.
    /// <c>Type</c> names the error and <c>Errors</c> contains user-facing messages.
    /// </summary>
    public class ErrorResponse
    {
        public string Type { get; set; } = string.Empty;   
        public List<string> Errors { get; set; } = new(); 
    }
}
