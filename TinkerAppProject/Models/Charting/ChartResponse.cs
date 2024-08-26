namespace TinkerAppProject.Models.Charting
{
    public class ChartResponse
    {
        public ChartTypeEnum Type { get; set; }
        public List<string> Labels { get; set; } = [];
        public LabelTypeEnum LabelType { get; set; }
        public List<int> Dataset { get; set; } = [];
        public int MonthRange { get; set; }
        public List<int> Values { get; set; }
    }
}
