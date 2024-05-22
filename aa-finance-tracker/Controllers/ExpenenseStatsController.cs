using AAExpenseTracker.Domain.Entities;
using AAFinanceTracker.Infrastructure.Repositories.Expense;
using Microsoft.AspNetCore.Mvc;

namespace AAFinanceTracker.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpenseStatsController(IExpenseRepository _expenseRepository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Expense>>> GetExpensesByCategoryYearMonth(string category, int year, int? month, CancellationToken cancellationToken)
    {
        var expenses = await _expenseRepository.GetExpensesByCategoryYearMonth(category, year, month, cancellationToken);

        if (expenses.Count == 0)
        {
            return NotFound();
        }

        return Ok(expenses);
    }
}
