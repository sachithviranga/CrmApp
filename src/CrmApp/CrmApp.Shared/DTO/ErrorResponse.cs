using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmApp.Shared.DTO
{
    public class ErrorResponse
    {
        public string Type { get; set; } = string.Empty;   
        public List<string> Errors { get; set; } = new(); 
    }
}
