namespace AAFinanceTracker.Infrastructure.Repositories.Expense;

public interface IExpenseRepository
{
    public Task<List<AAFinanceTracker.Domain.Entities.Expense>> GetExpensesByTimeframe(DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
    public Task<List<AAFinanceTracker.Domain.Entities.Expense>> GetExpensesByCategoryYearMonth(string categoryName, int year,int? month, CancellationToken cancellationToken);
}