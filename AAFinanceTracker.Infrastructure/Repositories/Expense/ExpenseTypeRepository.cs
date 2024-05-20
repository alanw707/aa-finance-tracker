using AAExpenseTracker.Domain.Data;
using AAExpenseTracker.Domain.Entities;

namespace AAFinanceTracker.Infrastructure.Repositories.Expense
{
    public class ExpenseTypeRepository(FinanceTrackerDbContext context) 
        : GenericRepository<ExpenseType>(context)
    {
    }
}
