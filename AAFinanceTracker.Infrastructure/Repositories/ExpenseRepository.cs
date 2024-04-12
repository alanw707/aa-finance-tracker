using AAExpenseTracker.Domain.Data;
using AAExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace AAFinanceTracker.Infrastructure.Repositories;

public class ExpenseRepository : GenericRepository<Expense>
{
    public ExpenseRepository(FinanceTrackerDbContext context) : base(context)
    {

    }
}