namespace CrmApp.Shared.DTO
{
    public class ErrorResponse
    {
        public string Type { get; set; } = string.Empty;   
        public List<string> Errors { get; set; } = new(); 
    }
}
