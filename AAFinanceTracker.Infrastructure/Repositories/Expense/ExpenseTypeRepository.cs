using AAFinanceTracker.Domain.Data;
using AAFinanceTracker.Domain.Entities;

namespace AAFinanceTracker.Infrastructure.Repositories.Expense;

public class ExpenseTypeRepository(FinanceTrackerDbContext context) 
    : GenericRepository<ExpenseType>(context)
{
}

