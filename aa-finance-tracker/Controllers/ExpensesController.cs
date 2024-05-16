using Microsoft.AspNetCore.Mvc;
using AAFinanceTracker.API.Models;
using AAExpenseTracker.Domain.Entities;
using AAFinanceTracker.Infrastructure.Repositories;

namespace AAFinanceTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController(IServiceProvider services) : ControllerBase
    {

        // GET: api/Expenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
        {
            var expenseRepository = services.GetRequiredService<IExpenseRepository>();
            var expenses = await expenseRepository.GetExpensesByTimeframe(startDate, endDate, cancellationToken);

            if (expenses.Count() == 0) return NotFound(); // Handle not found scenario here.


            return Ok(expenses);
        }

        // GET: api/Expenses/5
        [HttpGet("{expenseId}")]
        public async Task<ActionResult<Expense>> GetExpense(int expenseId, CancellationToken cancellationToken)
        {
            var expenseRepository = services.GetRequiredService<IRepository<Expense>>();
            var expense = await expenseRepository
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

            var expenseRepository = services.GetRequiredService<IRepository<Expense>>();

            try
            {
                expenseRepository.Update(expense);

                await expenseRepository.SaveChangesAsync(cancellationToken);

                return NoContent();
            }
            catch (Exception)
            {
                if (!ExpenseExists(id, expenseRepository))
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

            var expenseRepository = services.GetRequiredService<IRepository<Expense>>();
            var expenseTypesRepo = services.GetRequiredService<IRepository<ExpenseType>>();
            var expenseCategoryRepo = services.GetRequiredService<IRepository<ExpenseCategory>>();


            if (expenseModel is null) { return BadRequest("ExpenseModel is null"); }

            var expense = new Expense()
            {
                ExpenseCategoryName = expenseModel.CategoryName,
                ExpenseTypeName = expenseModel.TypeName,
                Comments = expenseModel.Comments,
                Amount = expenseModel.Amount,
                Date = DateTime.Now
            };

            var existingExpenseType = await expenseTypesRepo.Get(expenseModel.TypeName, cancellationToken);

            if (existingExpenseType != null)
            {
                expense.ExpenseType = existingExpenseType;
            }
            else
            {
                expense.ExpenseType = new ExpenseType() { Name = expenseModel.TypeName };
            }

            var existingExpenseCategory = await expenseCategoryRepo.Get(expenseModel.CategoryName, cancellationToken);

            if (existingExpenseCategory != null)
            {
                expense.ExpenseCategory = existingExpenseCategory;
            }
            else
            {
                expense.ExpenseCategory = new ExpenseCategory() { Name = expenseModel.CategoryName };
            }

            await expenseRepository.Add(expense, cancellationToken);

            await expenseRepository.SaveChangesAsync(cancellationToken);

            return CreatedAtAction("GetExpense", new { expenseId = expense.ExpenseId }, expense);
        }

        // DELETE: api/Expenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id, CancellationToken cancellationToken)
        {
            var expenseRepository = services.GetRequiredService<IRepository<Expense>>();
            var expense = await expenseRepository.Find(e => e.ExpenseId == id, cancellationToken);

            if (expense == null)
            {
                return NotFound();
            }

            expenseRepository.Delete(expense.Single());
            await expenseRepository.SaveChangesAsync(cancellationToken);
            expenseRepository.Delete(expense.Single());
            await expenseRepository.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        private bool ExpenseExists(int id, IRepository<Expense> expenseRepository)
        {
            return expenseRepository.Find(e => e.ExpenseId == id, CancellationToken.None).Result.Count != 0;
        }
    }
}
