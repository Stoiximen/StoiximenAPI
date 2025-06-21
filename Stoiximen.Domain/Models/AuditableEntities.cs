using System.ComponentModel.DataAnnotations.Schema;

namespace Stoiximen.Domain.Models
{
    public abstract class AuditableEntity
    {
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }

    public abstract class SoftDeletableEntity : AuditableEntity
    {
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
        [NotMapped]
        public bool IsActive => !DeletedAt.HasValue;
    }
}
