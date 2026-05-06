namespace DigitalWorkshop.Domain.Entities
{
    public class BomItem
    {
        public int Id { get; set; }
        public int TransitionId { get; set; }
        public Transition Transition { get; set; } = null!;

        public string PartNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = "шт";
    }
}