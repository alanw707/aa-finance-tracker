using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AAExpenseTracker.Domain.Entities;
using AAFinanceTracker.Infrastructure.Repositories;

namespace AAFinanceTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseCategoriesController : ControllerBase
    {
        private readonly IRepository<ExpenseCategory> _expenseCategoriesRepository;

        public ExpenseCategoriesController(IRepository<ExpenseCategory> expenseCategoriesRepository)
        {
            _expenseCategoriesRepository = expenseCategoriesRepository;
        }

        // GET: api/ExpenseCategories
        [HttpGet]
        public async Task<List<ExpenseCategory>> GetExpensesCategories(CancellationToken cancellationToken)
        {
            return await _expenseCategoriesRepository.All(cancellationToken);
        }

        // GET: api/ExpenseCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseCategory>> GetExpenseCategory(string id, CancellationToken cancellation)
        {
            var expenseCategory = _expenseCategoriesRepository
                .Find(c => c.Name == id, cancellation)
                .Result.Single();
            
            if (expenseCategory == null)
            {
                return NotFound();
            }

            return expenseCategory;
        }

        // PUT: api/ExpenseCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpenseCategory(string id, ExpenseCategory expenseCategory, CancellationToken cancellation)
        {

            var existingCategory = _expenseCategoriesRepository
                .Find(ca => ca.Name == id, cancellation)
                .Result.Single();

            if (existingCategory is null)
            {
                return BadRequest();
            }

            _expenseCategoriesRepository.Update(expenseCategory);
            await _expenseCategoriesRepository.SaveChangesAsync(cancellation);

            return NoContent();
        }

        // POST: api/ExpenseCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExpenseCategory>> PostExpenseCategory(ExpenseCategory expenseCategory, CancellationToken cancellation)
        {
            try
            {
                _expenseCategoriesRepository.Add(expenseCategory,cancellation);
                await _expenseCategoriesRepository.SaveChangesAsync(cancellation);
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

            return CreatedAtAction("GetExpenseCategory", new { id = expenseCategory.Name }, expenseCategory);
        }

        // DELETE: api/ExpenseCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpenseCategory(string id, CancellationToken cancellation)
        {
            var expenseCategory = _expenseCategoriesRepository
                    .Find(ca => ca.Name == id,cancellation)
                    .Result.Single();

            if (expenseCategory == null)
            {
                return NotFound();
            }

            await _expenseCategoriesRepository.SaveChangesAsync(cancellation);

            return NoContent();
        }

        private bool ExpenseCategoryExists(string id, CancellationToken token)
        {
            return _expenseCategoriesRepository.Find(e => e.Name == id, token)
                .Result.Count > 0;
        }
    }
}
