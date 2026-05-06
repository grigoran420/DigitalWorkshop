using System;
using System.Collections.Generic;

namespace DigitalWorkshop.Domain.Entities
{
    public class TechnologyProcess
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Версионность
        public int VersionMajor { get; set; } = 1;
        public int VersionMinor { get; set; } = 0;
        public string Version => $"{VersionMajor}.{VersionMinor}";
        public string? VersionHash { get; set; }
        public bool IsLocked { get; set; } = false;

        // Статус и согласование
        public TpStatus Status { get; set; } = TpStatus.Draft;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ApprovedAt { get; set; }

        // Связи
        public ICollection<Operation> Operations { get; set; } = new List<Operation>();
        public ICollection<AuditLog> History { get; set; } = new List<AuditLog>();

        public void AddHistoryEntry(string action, string user, string? details = null)
        {
            History.Add(new AuditLog
            {
                TechnologyProcessId = this.Id,
                Action = action,
                UserName = user,
                Details = details,
                Timestamp = DateTime.UtcNow
            });
        }
    }

    public enum TpStatus
    {
        Draft,              // Черновик
        PendingApproval,    // На согласовании (Технолог -> Ведущий)
        ApprovedByLead,     // Согласовано ведущим (ожидает Главного)
        Approved,           // Утверждено (Главным инженером)
        Rejected,           // Отклонено
        Archived            // Архивная версия
    }
}