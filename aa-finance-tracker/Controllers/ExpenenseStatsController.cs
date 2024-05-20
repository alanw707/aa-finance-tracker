using AAExpenseTracker.Domain.Entities;
using AAFinanceTracker.Infrastructure.Repositories.Expense;
using Microsoft.AspNetCore.Mvc;

namespace AAFinanceTracker.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpenseStatsController(IExpenseRepository _expenseRepository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Expense>>> GetExpensesByCategoryYear(string category, int year, CancellationToken cancellationToken)
    {
        var expenses = await _expenseRepository.GetExpensesByCategoryYear(category, year, cancellationToken);

        if (expenses.Count == 0)
        {
            return NotFound();
        }

        return Ok(expenses);
    }
}
