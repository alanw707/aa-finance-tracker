using AAExpenseTracker.Domain.Data;
using AAExpenseTracker.Domain.Entities;

namespace AAFinanceTracker.Infrastructure.Repositories
{
    public class ExpenseTypeRepository(FinanceTrackerDbContext context) 
        : GenericRepository<ExpenseType>(context)
    {
    }
}
