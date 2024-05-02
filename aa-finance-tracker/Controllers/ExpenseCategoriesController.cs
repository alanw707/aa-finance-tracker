using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AAExpenseTracker.Domain.Entities;
using AAFinanceTracker.Infrastructure.Repositories;

namespace AAFinanceTracker.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpenseCategoriesController(IRepository<ExpenseCategory> expenseCategoriesRepository) : ControllerBase
{

    // GET: api/ExpenseCategories
    [HttpGet]
    public async Task<ActionResult<List<ExpenseCategory>>> GetExpensesCategories(CancellationToken cancellationToken)
    {
        var result = await expenseCategoriesRepository.All(cancellationToken);

        return Ok(result);
    }

    // GET: api/ExpenseCategories/5
    [HttpGet("{name}")]
    public async Task<ActionResult<ExpenseCategory>> GetExpenseCategory(string name, CancellationToken cancellation)
    {
        var expenseCategory = await expenseCategoriesRepository
            .Find(c => c.Name == name, cancellation);

        return expenseCategory.Single();
    }

    // PUT: api/ExpenseCategories/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutExpenseCategory(string id, ExpenseCategory expenseCategory, CancellationToken cancellation)
    {

        var existingCategory = expenseCategoriesRepository
            .Find(ca => ca.Name == id, cancellation)
            .Result.Single();

        if (existingCategory is null)
        {
            return BadRequest();
        }

        expenseCategoriesRepository.Update(expenseCategory);
        await expenseCategoriesRepository.SaveChangesAsync(cancellation);

        return NoContent();
    }

    // POST: api/ExpenseCategories
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<ExpenseCategory>> PostExpenseCategory(ExpenseCategory expenseCategory, CancellationToken cancellation)
    {
        try
        {
            await expenseCategoriesRepository.Add(expenseCategory, cancellation);
            await expenseCategoriesRepository.SaveChangesAsync(cancellation);
        }
        catch (DbUpdateException)
        {
            if (ExpenseCategoryExists(expenseCategory.Name, cancellation))
            {
                return BadRequest();
            }
            else
            {
                throw;
            }
        }

        return CreatedAtAction("GetExpenseCategory", new { name = expenseCategory.Name }, expenseCategory);
    }

    // DELETE: api/ExpenseCategories/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpenseCategory(string id, CancellationToken cancellation)
    {
        var expenseCategory = expenseCategoriesRepository
            .Find(ca => ca.Name == id, cancellation)
            .Result.Single();

        if (expenseCategory == null)
        {
            return NotFound();
        }

        expenseCategoriesRepository.Delete(expenseCategory);

        await expenseCategoriesRepository.SaveChangesAsync(cancellation);

        return NoContent();
    }

    private bool ExpenseCategoryExists(string id, CancellationToken token)
    {
        return expenseCategoriesRepository.Find(e => e.Name == id, token)
            .Result.Count > 0;
    }
}