using Entities = AAFinanceTracker.Domain.Entities;


namespace AAFinanceTracker.Infrastructure.Repositories.Investment;

public interface IInvestmentRepository
{
    public Task<List<Entities.Investment>> GetInvestmentsTimeframe(DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
    public Task<List<Entities.Investment>> GetInvestmentsByTypeYearMonth(string typeName, int year, int? month, CancellationToken cancellationToken);
}