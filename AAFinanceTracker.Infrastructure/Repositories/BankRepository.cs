using AAExpenseTracker.Domain.Data;
using AAExpenseTracker.Domain.Entities;

namespace AAFinanceTracker.Infrastructure.Repositories;

public class BankRepository(FinanceTrackerDbContext context)
        : GenericRepository<Bank>(context)
{ }

