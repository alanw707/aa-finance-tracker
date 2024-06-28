using AAFinanceTracker.Domain.Data;
using AAFinanceTracker.Domain.Entities;

namespace AAFinanceTracker.Infrastructure.Repositories.CustodianBank;

public class CustodianBankRepository (FinanceTrackerDbContext context)
    : GenericRepository<InvestmentType>(context)
{
}