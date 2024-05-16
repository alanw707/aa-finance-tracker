using AAExpenseTracker.Domain.Entities;

namespace AAFinanceTracker.Infrastructure.Repositories;

public interface IExpenseRepository
{
    public Task<List<Expense>> GetExpensesByTimeframe(DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
}