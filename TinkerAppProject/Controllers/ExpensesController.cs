using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TinkerAppProject.Areas.Identity.Data;
using TinkerAppProject.Models.Expenses;
using TinkerAppProject.Repositories;

namespace TinkerAppProject.Controllers
{
    [Authorize]
    public class ExpensesController : Controller
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly UserManager<TinkerAppProjectUser> _userManager;
        private readonly ILogger<ExpensesController> _logger;

        public ExpensesController(IExpenseRepository expenseRepository, UserManager<TinkerAppProjectUser> userManager, ILogger<ExpensesController> logger)
        {
            _expenseRepository = expenseRepository;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            var expenses = new ExpensesViewModel();
            try
            {
                var user = await _userManager.GetUserAsync(User);

                if (string.IsNullOrEmpty(searchString))
                {
                    var allExpenses = await _expenseRepository.GetAllExpensesByUser(user.Id);
                    expenses.Expenses.AddRange(allExpenses);
                }
                else
                {
                    var filteredExpenses = await _expenseRepository.GetExpensesByConceptAsync(searchString, user.Id);
                    expenses.Expenses.AddRange(filteredExpenses);
                }

                return View(expenses);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = "Error loading user expenses. Message: " + ex.Message;
                _logger.LogError(ex, "Error loading user expenses.");
                return View("CreateExpenseError");
            }
        }

        public IActionResult CreateExpenseIndex()
        {
            return View(new ExpenseModel() { DayPaid = DateTime.Today});
        }

        public async Task<IActionResult> EditExpenseIndex(Guid expenseGuid)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var model = await _expenseRepository.GetExpenseById(expenseGuid);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpense(ExpenseModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            int response;
            try
            {
                var user = await _userManager.GetUserAsync(User);

                if (user == null)
                {
                    return RedirectToAction("Index");
                }
                model.UserId = user.Id;
                model.UserName = user.UserName;
                model.User = user;
                response = await _expenseRepository.CreateExpense(model);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error creating new expense.");
                ViewBag.Exception = "Error creating new expense. Message: " + ex.Message;
                return View("CreateExpenseError");
            }

            return response == 1 ? View("CreateExpenseSuccess") : View("CreateExpenseError");
        }

        public async Task<IActionResult> EditExpense(ExpenseModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            try
            {
                var response = await _expenseRepository.UpdateExpense(model);
                if (response == 1)
                {
                    return View("CreateExpenseSuccess");
                }
                else
                {
                    throw new Exception("Couldn't update expense.");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error updating expense.");
                ViewBag.Exception = "Error updating expense. Message: " + ex.Message;
                return View("CreateExpenseError");
            }

        }
        
        public async Task<IActionResult> DeleteExpense(Guid expenseGuid)
        {
            return await _expenseRepository.DeleteExpense(expenseGuid) == 1 ? RedirectToAction("Index") : View("CreateExpenseError");
        }
    }
}
