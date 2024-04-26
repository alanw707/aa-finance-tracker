using AAExpenseTracker.Domain.Data;
using AAExpenseTracker.Domain.Entities;
namespace AAFinanceTracker.Infrastructure.Repositories;

public class ExpenseRepository(FinanceTrackerDbContext context)
: GenericRepository<Expense>(context)
{

}