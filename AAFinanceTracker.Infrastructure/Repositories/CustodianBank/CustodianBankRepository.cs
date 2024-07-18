using AAFinanceTracker.Domain.Data;

namespace AAFinanceTracker.Infrastructure.Repositories.CustodianBank;

public class CustodianBankRepository (FinanceTrackerDbContext context)
    : GenericRepository<Domain.Entities.CustodianBank>(context)
{
}