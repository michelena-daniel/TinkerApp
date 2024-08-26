using System.ComponentModel.DataAnnotations;

namespace TinkerAppProject.Models.Charting
{
    public class ChartModel
    {
        public ChartTypeEnum Type { get; set; }
        [Range(1, 100)]
        public int MonthRange { get; set; }
        public LabelTypeEnum LabelType { get; set; }
    }
}
