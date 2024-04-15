using AAExpenseTracker.Domain.Entities;
using AAFinanceTracker.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AAFinanceTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseTypesController : ControllerBase
    {
        private readonly IRepository<ExpenseType> _repo;

        public ExpenseTypesController(IRepository<ExpenseType> repo)
        {
            _repo = repo;
        }

        // GET: api/ExpenseTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseType>>> GetExpenseTypes(CancellationToken token)
        {
            return await _repo.All(token);
        }

        // GET: api/ExpenseTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseType>> GetExpenseType(string id, CancellationToken token)
        {
            var expenseType = await _repo.Find(et => et.Name == id, token);

            if (expenseType == null)
            {
                return NotFound();
            }

            return Ok(expenseType);
        }

        // PUT: api/ExpenseTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpenseType(string id, ExpenseType expenseType, CancellationToken token)
        {
            if (id != expenseType.Name)
            {
                return BadRequest();
            }

            try
            {
                _repo.Update(expenseType);
                await _repo.SaveChangesAsync(token);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseTypeExists(id, token))
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

        // POST: api/ExpenseTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExpenseType>> PostExpenseType(ExpenseType expenseType, CancellationToken token)
        {
            try
            {
                await _repo.Add(expenseType, token);
                await _repo.SaveChangesAsync(token);
            }
            catch (DbUpdateException)
            {
                if (ExpenseTypeExists(expenseType.Name, token))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetExpenseType", new { id = expenseType.Name }, expenseType);
        }

        // DELETE: api/ExpenseTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpenseType(string id, CancellationToken token)
        {
            var expenseType = _repo.Find(et=>et.Name == id,token).Result.SingleOrDefault();
            if (expenseType == null)
            {
                return NotFound();
            }

            _repo.Delete(expenseType);
            await _repo.SaveChangesAsync(token);

            return NoContent();
        }

        private bool ExpenseTypeExists(string id, CancellationToken token)
        {
            return _repo.Find(e => e.Name == id,token).Result.Count > 0;
        }
    }
}
