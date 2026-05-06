// Entities/Transition.cs
using System.Collections.Generic;

namespace DigitalWorkshop.Domain.Entities
{
    public class Transition
    {
        public int Id { get; set; }
        public int OperationId { get; set; }
        public Operation Operation { get; set; } = null!;

        public string Description { get; set; } = string.Empty;
        public string? MediaUrl { get; set; } // Путь к фото/видео

        // Параметры контроля
        public decimal? MinParam { get; set; }
        public decimal? MaxParam { get; set; }
        public string? ParamUnit { get; set; }

        // Ресурсы (BOM и Инструмент)
        public ICollection<BomItem> BomItems { get; set; } = new List<BomItem>();
        public ICollection<ToolRequirement> Tools { get; set; } = new List<ToolRequirement>();
    }
}