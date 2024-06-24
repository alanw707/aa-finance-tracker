using AAFinanceTracker.Domain.Data;
using AAFinanceTracker.Domain.Entities;

namespace AAFinanceTracker.Infrastructure.Repositories.Expense;

public class ExpenseCategoryRepository(FinanceTrackerDbContext context)
    : GenericRepository<ExpenseCategory>(context)
{
}

