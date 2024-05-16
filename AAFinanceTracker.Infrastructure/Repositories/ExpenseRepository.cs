using AAExpenseTracker.Domain.Data;
using AAExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AAFinanceTracker.Infrastructure.Repositories;

public class ExpenseRepository(FinanceTrackerDbContext context)
: GenericRepository<Expense>(context), IExpenseRepository
{
    public async Task<List<Expense>> GetExpensesByTimeframe(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
    {
        var expenses = await context.Set<Expense>()
            .Where(e => e.Date >= startDate && e.Date <= endDate)
            .ToListAsync(cancellationToken);

        return expenses;
    }
}