using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinkerAppProject.Models;

namespace TinkerAppProject.Controllers
{
    [Authorize]
    public class ExpensesController : Controller
    {
        public IActionResult Index()
        {
            var expenses = new ExpensesViewModel();
            for (int i = 0; i < 200; i++)
            {
                Random random = new Random();
                var categoryValues = Enum.GetValues(typeof(CategoryEnum));
                var expense = new ExpenseModel
                {
                    Id = i,
                    UserId = i,
                    UserName = "UserNameTest",
                    Concept = $"Test {i}",
                    Category = (CategoryEnum)categoryValues.GetValue(random.Next(categoryValues.Length)),
                    AmountPaid = 100,
                    DayPaid = DateTime.Now,
                    DayRegistered = DateTime.Now
                };
                expenses.Expenses.Add(expense);
            }
            return View(expenses);
        }
    }
}
