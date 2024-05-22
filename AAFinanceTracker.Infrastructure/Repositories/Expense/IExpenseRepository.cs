namespace AAFinanceTracker.Infrastructure.Repositories.Expense;

public interface IExpenseRepository
{
    public Task<List<AAExpenseTracker.Domain.Entities.Expense>> GetExpensesByTimeframe(DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
    public Task<List<AAExpenseTracker.Domain.Entities.Expense>> GetExpensesByCategoryYearMonth(string categoryName, int year,int? month, CancellationToken cancellationToken);
}