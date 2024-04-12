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
        public async Task<ActionResult<IEnumerable<ExpenseCategory>>> GetExpensesCategories(CancellationToken cancellationToken)
        {
            return await _expenseCategoriesRepository.All().ToListAsync(cancellationToken);
        }

        // GET: api/ExpenseCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseCategory>> GetExpenseCategory(string id, CancellationToken cancellation)
        {
            var expenseCategory = await _expenseCategoriesRepository
                .Find(c => c.Name == id)
                .SingleAsync(cancellation);

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
                .Find(ca => ca.Name == id)
                .SingleAsync(cancellation).Result;

            if (existingCategory is null)
            {
                return BadRequest();
            }

            _expenseCategoriesRepository.Update(expenseCategory);
            _expenseCategoriesRepository.SaveChanges();

            return NoContent();
        }

        // POST: api/ExpenseCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExpenseCategory>> PostExpenseCategory(ExpenseCategory expenseCategory, CancellationToken cancellation)
        {
            try
            {
                _expenseCategoriesRepository.Add(expenseCategory);
                _expenseCategoriesRepository.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ExpenseCategoryExists(expenseCategory.Name))
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
            var expenseCategory =
                await _expenseCategoriesRepository
                    .Find(ca => ca.Name == id)
                    .SingleAsync(cancellation);

            if (expenseCategory == null)
            {
                return NotFound();
            }

            _expenseCategoriesRepository.Delete(expenseCategory);
            _expenseCategoriesRepository.SaveChanges();

            return NoContent();
        }

        private bool ExpenseCategoryExists(string id)
        {
            return _expenseCategoriesRepository.Find(e => e.Name == id).Any();
        }
    }
}
