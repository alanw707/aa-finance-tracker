using Microsoft.EntityFrameworkCore;
using AAExpenseTracker.Domain.Data;

namespace AAFinanceTracker.Infrastructure.Repositories.Expense;

public class ExpenseRepository(FinanceTrackerDbContext context)
: GenericRepository<AAExpenseTracker.Domain.Entities.Expense>(context), IExpenseRepository
{
    public async Task<List<AAExpenseTracker.Domain.Entities.Expense>> GetExpensesByCategoryYearMonth(string categoryName, int year,int? month, CancellationToken cancellationToken)
    {
        return await context.Set<AAExpenseTracker.Domain.Entities.Expense>()
            .Where(expense => expense.ExpenseCategoryName == categoryName && 
                    expense.Date.Year == year && 
                    (month == null || expense.Date.Month == month))
            .ToListAsync(cancellationToken);
    }

    public async Task<List<AAExpenseTracker.Domain.Entities.Expense>> GetExpensesByTimeframe(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
    {
        return await context.Set<AAExpenseTracker.Domain.Entities.Expense>()
            .Where(e => e.Date >= startDate && e.Date <= endDate)
            .ToListAsync(cancellationToken);
    }
}