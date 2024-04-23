using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AAFinanceTracker.API.Models;
using AAExpenseTracker.Domain.Entities;
using AAFinanceTracker.Infrastructure.Repositories;

namespace AAFinanceTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController(IRepository<Expense> _expenseRepository) : ControllerBase
    {

        // GET: api/Expenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses(CancellationToken cancellationToken)
        {
            var expenses = await _expenseRepository.All(cancellationToken);

            return expenses;
        }

        // GET: api/Expenses/5
        [HttpGet("{expenseId}")]
        public async Task<ActionResult<Expense>> GetExpense(int expenseId, CancellationToken cancellationToken)
        {
            var expense = await _expenseRepository
                .Find(e => e.ExpenseId == expenseId, cancellationToken);

            if (expense.Count < 1)
            {
                return NotFound();
            }

            return expense.Single();
        }

        // PUT: api/Expenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpense(int id, Expense expense, CancellationToken cancellationToken)
        {
            if (id != expense.ExpenseId)
            {
                return BadRequest();
            }

            try
            {
                _expenseRepository.Delete(expense);

                await _expenseRepository.SaveChangesAsync(cancellationToken);

                return NoContent();
            }
            catch (Exception)
            {
                if (!ExpenseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Expenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExpenseModel>> PostExpense(ExpenseModel expenseModel, CancellationToken cancellationToken)
        {
            var expense = new Expense()
            {
                ExpenseCategoryName = expenseModel.CategoryName,
                ExpenseTypeName = expenseModel.TypeName,
                Comments = expenseModel.Comments,
                Amount = expenseModel.Amount
            };

            var addedExpense = await _expenseRepository.Add(expense,cancellationToken);

            await _expenseRepository.SaveChangesAsync(cancellationToken);

            return CreatedAtAction("GetExpense", new { expenseId = expense.ExpenseId }, expense);
        }

        // DELETE: api/Expenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id, CancellationToken cancellationToken)
        {
            var expense = await _expenseRepository.Find(e=>e.ExpenseId == id, cancellationToken);

            if (expense == null)
            {
                return NotFound();
            }

            _expenseRepository.Delete(expense.Single());
            await _expenseRepository.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        private bool ExpenseExists(int id)
        {
            return _expenseRepository.Find(e => e.ExpenseId == id, CancellationToken.None).Result.Count != 0;
        }
    }
}
