using Microsoft.EntityFrameworkCore;
using TinkerAppProject.Data;
using TinkerAppProject.Models.Expenses;

namespace TinkerAppProject.Repositories
{
    public interface IExpenseRepository
    {
        Task<List<ExpenseModel>> GetAllExpensesByUser(string userId);
        Task<ExpenseModel> GetExpenseById(Guid expenseGuid);
        Task<int> CreateExpense(ExpenseModel expense);
        Task<int> DeleteExpense(Guid expenseGuid);
        Task<int> UpdateExpense(ExpenseModel expense);

    }
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ExpenseRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<List<ExpenseModel>> GetAllExpensesByUser(string userId)
        {
            return await _dbContext.Expense
                .Where(e => e.UserId == userId)
                .ToListAsync();
        }

        public async Task<ExpenseModel> GetExpenseById(Guid expenseGuid)
        {
            return await _dbContext.Expense.SingleAsync(e => e.Id == expenseGuid);
        }

        public async Task<int> CreateExpense(ExpenseModel expense)
        {
            await _dbContext.Expense.AddAsync(expense);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateExpense(ExpenseModel expense)
        {
            return await _dbContext.Expense
                .Where(e => e.Id == expense.Id)
                .ExecuteUpdateAsync(e => e
                .SetProperty(e => e.Concept, expense.Concept)
                .SetProperty(e => e.AmountPaid, expense.AmountPaid)
                .SetProperty(e => e.Category, expense.Category)
                .SetProperty(e => e.DayPaid, expense.DayPaid));
        }

        public async Task<int> DeleteExpense(Guid expenseGuid)
        {
            return await _dbContext.Expense.Where(e => e.Id == expenseGuid).ExecuteDeleteAsync();
        }
    }
}
