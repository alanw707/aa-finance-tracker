using AAExpenseTracker.Domain.Data;
using AAExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AAFinanceTracker.Infrastructure.Repositories;

public class ExpenseRepository(FinanceTrackerDbContext context)
: GenericRepository<Expense>(context), IExpenseRepository
{
    public async Task<List<Expense>> GetExpensesByCategoryYear(string categoryName, int year, CancellationToken cancellationToken)
    {
        return await context.Set<Expense>()
            .Where(expense => expense.ExpenseCategoryName == categoryName && expense.Date.Year == year)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Expense>> GetExpensesByTimeframe(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
    {
        return await context.Set<Expense>()
            .Where(e => e.Date >= startDate && e.Date <= endDate)
            .ToListAsync(cancellationToken);
    }
}