using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TinkerAppProject.Areas.Identity.Data;
using TinkerAppProject.Models;
using TinkerAppProject.Models.Expenses;
using TinkerAppProject.Repositories;

namespace TinkerAppProject.Controllers
{
    [Authorize]
    public class ExpensesController : Controller
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly UserManager<TinkerAppProjectUser> _userManager;

        public ExpensesController(IExpenseRepository expenseRepository, UserManager<TinkerAppProjectUser> userManager)
        {
            _expenseRepository = expenseRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var expenses = new ExpensesViewModel();
            try
            {                
                var user = await _userManager.GetUserAsync(User);
                var expenseList = await _expenseRepository.GetAllExpensesByUser(user.Id);
                expenses.Expenses.AddRange(expenseList);
                return View(expenses);
            }
            catch(Exception ex)
            {
                ViewBag.Exception = "Error loading user expenses. Message: " + ex.Message;
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
                model.DayPaid = DateTime.Now;
                response = await _expenseRepository.CreateExpense(model);
            }
            catch(Exception ex)
            {
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
