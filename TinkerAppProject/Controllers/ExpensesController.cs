using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TinkerAppProject.Areas.Identity.Data;
using TinkerAppProject.Models;
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
            catch
            {
                //TODO handle error
                throw new Exception("Error loading user expenses");
            }            
        }

        public IActionResult CreateExpenseIndex()
        {
            return View(new ExpenseModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpense(ExpenseModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            var user = await _userManager.GetUserAsync(User);

            if(user != null)
            {
                model.UserId = user.Id;
                model.UserName = user.UserName;
                model.User = user;
                model.DayPaid = DateTime.Now;
            }
            else
            {
                return View("Index");
            }            
            var response = await _expenseRepository.CreateExpense(model);

            return response == 1 ? View("CreateExpenseSuccess") : View("CreateExpenseError");
        }
        
        public IActionResult DeleteExpense(Guid expenseGuid)
        {
            _expenseRepository.DeleteExpense(expenseGuid);
            return RedirectToAction("Index");
        }
    }
}
