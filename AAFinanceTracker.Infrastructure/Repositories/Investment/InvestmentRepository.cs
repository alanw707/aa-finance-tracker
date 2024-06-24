using AAFinanceTracker.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Entities = AAFinanceTracker.Domain.Entities;

namespace AAFinanceTracker.Infrastructure.Repositories.Investment;

public class InvestmentRepository(FinanceTrackerDbContext context)
        : GenericRepository<Entities.Investment>(context), IInvestmentRepository
{             
    public async Task<List<Entities.Investment>> GetInvestmentsByTypeYearMonth(string typeName, int year, int? month, CancellationToken cancellationToken)
    {
         return await context.Set<Entities.Investment>()
            .Where(investment => investment.InvestmentTypeName == typeName && 
                    investment.DateAdded.Year == year && (month == null || investment.DateAdded.Month == month))
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Entities.Investment>> GetInvestmentsTimeframe(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
    {
          return await context.Set<Entities.Investment>()
            .Where(e => e.DateAdded >= startDate && e.DateAdded <= endDate)
            .ToListAsync(cancellationToken);
    }
}

