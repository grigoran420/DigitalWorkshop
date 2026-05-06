// Entities/Operation.cs
using System.Collections.Generic;

namespace DigitalWorkshop.Domain.Entities
{
    public class Operation
    {
        public int Id { get; set; }
        public int TechnologyProcessId { get; set; }
        public TechnologyProcess TechnologyProcess { get; set; } = null!;

        public string Code { get; set; } = string.Empty; // Например "005"
        public string Name { get; set; } = string.Empty;
        public int NormTimeMinutes { get; set; }
        public string? RequiredQualification { get; set; }
        public string? WorkCenter { get; set; }

        public ICollection<Transition> Transitions { get; set; } = new List<Transition>();
    }
}

