using AAFinanceTracker.Domain.Data;
using AAFinanceTracker.Domain.Entities;

namespace AAFinanceTracker.Infrastructure.Repositories.Investment;

public class InvestmentTypeRepository(FinanceTrackerDbContext context)
        : GenericRepository<InvestmentType>(context)
{
}
