using System;

namespace DigitalWorkshop.Domain.Entities
{
    public class AuditLog
    {
        public int Id { get; set; }
        public int TechnologyProcessId { get; set; }
        public TechnologyProcess TechnologyProcess { get; set; } = null!;

        public string Action { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string? Details { get; set; }
        public DateTime Timestamp { get; set; }
    }
}