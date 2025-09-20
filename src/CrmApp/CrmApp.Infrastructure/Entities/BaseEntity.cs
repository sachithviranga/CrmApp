using System.ComponentModel.DataAnnotations;

namespace CrmApp.Infrastructure.Entities
{
    public class BaseEntity
    {
        [Key]
        public int AccountId { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public DateTime? DateUpdated { get; set; } = DateTime.UtcNow;
    }
}
