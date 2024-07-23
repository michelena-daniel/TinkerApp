using Microsoft.EntityFrameworkCore;
using TinkerAppProject.Data;
using TinkerAppProject.Models;

namespace TinkerAppProject.Repositories
{
    public interface IExpenseRepository
    {
        Task<List<ExpenseModel>> GetAllExpensesByUser(string userId);
        Task<int> CreateExpense(ExpenseModel expense);

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

        public async Task<int> CreateExpense(ExpenseModel expense)
        {
            await _dbContext.Expense.AddAsync(expense);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
