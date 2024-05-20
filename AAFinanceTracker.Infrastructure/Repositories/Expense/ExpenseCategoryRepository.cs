using AAExpenseTracker.Domain.Data;
using AAExpenseTracker.Domain.Entities;

namespace AAFinanceTracker.Infrastructure.Repositories.Expense;

public class ExpenseCategoryRepository(FinanceTrackerDbContext context)
    : GenericRepository<ExpenseCategory>(context)
{
}

