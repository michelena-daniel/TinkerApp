namespace TinkerAppProject.Models.Charting
{
    public class ChartModel
    {
        public ChartTypeEnum Type { get; set; }
        public List<string> Labels { get; set; } = [];
        public List<int> Dataset { get; set; } = [];
    }
}
