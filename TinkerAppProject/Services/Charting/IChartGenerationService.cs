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
            var response = BuildBaseChart(model);

            if (!String.IsNullOrEmpty(userId))
            {
                var expenses = await _expenseRepository.GetAllExpensesByUser(userId);
                var startDate = DateTime.Now.AddMonths(-model.MonthRange + 1);
                var months = Enumerable.Range(0, model.MonthRange)
                    .Select(i => startDate.AddMonths(i))
                    .OrderByDescending(date => date);
                response.Dataset = months
                    .Select(month => expenses
                        .Where(expense => expense.DayPaid.Month == month.Month && expense.DayPaid.Year == month.Year)
                        .Sum(expense => expense.AmountPaid))
                    .ToList();
            }
            return response;
        }

        private static ChartResponse BuildBaseChart(ChartModel model)
        {
            return new ChartResponse
            {
                Type = model.Type,
                Labels = GetLastMonthsSelected(model.MonthRange),
                LabelType = model.LabelType
            };
        }

        private static List<string> GetLastMonthsSelected(int range)
        {
            var result = new List<string>();
            var date = DateTime.UtcNow;
            for (var i = 0; i < range; i++)
            {
                var sequencedDate = date.AddMonths(-i);
                result.Add(sequencedDate.ToString("MMMM")+$" {sequencedDate.Year.ToString().Substring(2)}");
            }
            return result;
        }
    }
}
