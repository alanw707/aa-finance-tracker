using AAExpenseTracker.Domain.Entities;
using AAFinanceTracker.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AAFinanceTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseTypesController(IRepository<ExpenseType> repo) : ControllerBase
    {
        
        // GET: api/ExpenseTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseType>?>> GetExpenseTypes(CancellationToken token)
        {
            return await repo.All(token);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseType>> GetExpenseType(string id, CancellationToken token)
        {
            var expenseType = await repo.Find(et => et.Name == id, token);

            if (expenseType.Count == 0)
            {
                return NotFound();
            }

            return Ok(expenseType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpenseType(string id, ExpenseType expenseType, CancellationToken token)
        {
            if (id != expenseType.Name)
            {
                return BadRequest();
            }

            repo.Update(expenseType);
            await repo.SaveChangesAsync(token);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ExpenseType>> PostExpenseType(ExpenseType expenseType, CancellationToken token)
        {
            try
            {
                await repo.Add(expenseType, token);
                await repo.SaveChangesAsync(token);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpenseType(string id, CancellationToken token)
        {
            var expenseType = repo.Find(et=>et.Name == id,token).Result.SingleOrDefault();
            if (expenseType == null)
            {
                return NotFound();
            }

            repo.Delete(expenseType);
            await repo.SaveChangesAsync(token);

            return NoContent();
        }

        private bool ExpenseTypeExists(string id, CancellationToken token)
        {
            return repo.Find(e => e.Name == id,token).Result.Count > 0;
        }
    }
}
