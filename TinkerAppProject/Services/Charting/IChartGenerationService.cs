using TinkerAppProject.Models.Charting;
using TinkerAppProject.Repositories;

namespace TinkerAppProject.Services.Charting
{
    public interface IChartGenerationService
    {
        Task<ChartResponse> GenerateChartByMonths(ChartModel model, string? userId);
    }

    public class ChartGenerationService : IChartGenerationService
    {
        private readonly IExpenseRepository _expenseRepository;

        public ChartGenerationService(IExpenseRepository expenseRepository) => _expenseRepository = expenseRepository;

        public async Task<ChartResponse> GenerateChartByMonths(ChartModel model, string? userId)
        {
            var response = new ChartResponse
            {
                Type = model.Type,
                Labels = GetLastMonthsSelected(model.MonthRange),
                LabelType = model.LabelType
            };

            if (!String.IsNullOrEmpty(userId))
            {
                var expenses = await _expenseRepository.GetAllExpensesByUser(userId);
                var startDate = DateTime.Now.AddMonths(-model.MonthRange + 1);

                response.Dataset = Enumerable.Range(0, model.MonthRange)
                        .Select(i => new { Year = startDate.AddMonths(i) })
                        .OrderByDescending(i => i.Year)
                        .ThenByDescending(i => i.Year.Month)
                        .Select(i => expenses.Where(e => e.DayPaid.Month == i.Year.Month && e.DayPaid.Year == i.Year.Year)
                        .Select(e => e.AmountPaid)
                        .Sum())
                        .ToList();
            }

            return response;
        }

        //public async Task<ChartResponse> AddBarChartInfo(ChartModel model, string? userId)
        //{
        //    var response = new ChartResponse
        //    {
        //        Type = model.Type,
        //        Labels = GetLastMonthsSelected(model.MonthRange)
        //    };

        //    if (!String.IsNullOrEmpty(userId))
        //    {
        //        switch (model.LabelType)
        //        {
        //            case LabelTypeEnum.Amount:
        //                var expenses = await _expenseRepository.GetAllExpensesByUser(userId);
        //                var startDate = DateTime.Now.AddMonths(-model.MonthRange + 1);
        //                response.Dataset = Enumerable.Range(0, model.MonthRange)
        //                    .Select(i => new { Year = startDate.AddMonths(i) })
        //                    .OrderByDescending(i => i.Year)
        //                    .ThenByDescending(i => i.Year.Month)
        //                    .Select(i => expenses.Where(e => e.DayPaid.Month == i.Year.Month).Select(e => e.AmountPaid).Sum())
        //                    .ToList();
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    return response;
        //}

        private static List<string> GetLastMonthsSelected(int range)
        {
            var result = new List<string>();
            var date = DateTime.UtcNow;
            for (var i = 0; i < range; i++)
            {
                result.Add(date.AddMonths(-i).ToString("MMMM"));
            }
            return result;
        }
    }
}
