namespace DigitalWorkshop.Domain.Entities
{
    public class ToolRequirement
    {
        public int Id { get; set; }
        public int TransitionId { get; set; }
        public Transition Transition { get; set; } = null!;

        public string ToolCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int? VerificationIntervalDays { get; set; } // Интервал поверки
    }
}