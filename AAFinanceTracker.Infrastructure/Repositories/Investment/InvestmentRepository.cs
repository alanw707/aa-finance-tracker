using AAExpenseTracker.Domain.Data;
using AAExpenseTracker.Domain.Entities;

namespace AAFinanceTracker.Infrastructure.Repositories;

public class InvestmentRepository(FinanceTrackerDbContext context)
        : GenericRepository<Investment>(context)
{ }

