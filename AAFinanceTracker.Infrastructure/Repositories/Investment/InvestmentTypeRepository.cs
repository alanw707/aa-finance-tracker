using AAExpenseTracker.Domain.Data;
using AAExpenseTracker.Domain.Entities;

namespace AAFinanceTracker.Infrastructure.Repositories.Investment;

public class InvestmentTypeRepository(FinanceTrackerDbContext context)
        : GenericRepository<InvestmentType>(context)
{
}
