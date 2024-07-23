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
        private IExpenseRepository _expenseRepository;
        private UserManager<TinkerAppProjectUser> _userManager;

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
            Random random = new Random();
            var user = await _userManager.GetUserAsync(User);
            model.UserId = user.Id;
            model.UserName = user.UserName;
            model.User = user;
            model.DayPaid = DateTime.Now;
            model.Id = random.Next(1,1000);
            var response = await _expenseRepository.CreateExpense(model);

            if(response == 1)
            {
                return View("CreateExpenseSuccess");
            }
            else
            {
                return View("CreateExpenseError");
            }
            
        }
    }
}
