using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using aa_finance_tracker.Data;
using aa_finance_tracker.Domains;

namespace aa_finance_tracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseCategoriesController : ControllerBase
    {
        private readonly FinanceTrackerDbContext _context;

        public ExpenseCategoriesController(FinanceTrackerDbContext context)
        {
            _context = context;
        }

        // GET: api/ExpenseCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseCategory>>> GetExpensesCategories(CancellationToken cancellation)
        {
            return await _context.ExpensesCategories.ToListAsync(cancellation);
        }

        // GET: api/ExpenseCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseCategory>> GetExpenseCategory(string id, CancellationToken cancellation)
        {
            var expenseCategory = await _context.ExpensesCategories.FindAsync(id,cancellation);

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
            if (id != expenseCategory.Name)
            {
                return BadRequest();
            }

            _context.Entry(expenseCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(cancellation);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ExpenseCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExpenseCategory>> PostExpenseCategory(ExpenseCategory expenseCategory, CancellationToken cancellation)
        {
            _context.ExpensesCategories.Add(expenseCategory);
            try
            {
                await _context.SaveChangesAsync(cancellation);
            }
            catch (DbUpdateException)
            {
                if (ExpenseCategoryExists(expenseCategory.Name))
                {
                    return Conflict();
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
            var expenseCategory = await _context.ExpensesCategories.FindAsync(id);
            if (expenseCategory == null)
            {
                return NotFound();
            }

            _context.ExpensesCategories.Remove(expenseCategory);
            await _context.SaveChangesAsync(cancellation);

            return NoContent();
        }

        private bool ExpenseCategoryExists(string id)
        {
            return _context.ExpensesCategories.Any(e => e.Name == id);
        }
    }
}
