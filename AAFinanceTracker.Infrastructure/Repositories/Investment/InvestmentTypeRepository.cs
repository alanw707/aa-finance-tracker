using AAExpenseTracker.Domain.Data;
using AAExpenseTracker.Domain.Entities;

namespace AAFinanceTracker.Infrastructure.Repositories;

public class InvestmentTypeRepository(FinanceTrackerDbContext context)
        : GenericRepository<InvestmentType>(context)
{
}
