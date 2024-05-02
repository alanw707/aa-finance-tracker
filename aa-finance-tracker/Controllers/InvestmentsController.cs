using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AAExpenseTracker.Domain.Entities;
using AAFinanceTracker.Infrastructure.Repositories;

namespace AAFinanceTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentsController(IRepository<Investment> _investmentRepository) : ControllerBase
    {
        // GET: api/Investments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Investment>>> GetInvestments(CancellationToken cancellationToken)
        {
            var expenses = await _investmentRepository.All(cancellationToken);

            return Ok(expenses);
        }

        // GET: api/Investments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Investment>> GetInvestment(string id, CancellationToken cancellationToken)
        {
            var investment = await _investmentRepository.Get(id, cancellationToken);

            if (investment is null)
            {
                return NotFound();
            }

            return Ok(investment);
        }

        // PUT: api/Investments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvestment(string Id, Investment investment, CancellationToken cancellationToken)
        {
            if (Id != investment.Id)
            {
                return BadRequest();
            }

            _investmentRepository.Update(investment);

            try
            {
                await _investmentRepository.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvestmentExists(Id, cancellationToken))
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

        // POST: api/Investments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Investment>> PostInvestment(Investment investment, CancellationToken cancellationToken)
        {
            await _investmentRepository.Add(investment, cancellationToken);

            try
            {
                await _investmentRepository.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException)
            {
                if (InvestmentExists(investment.Id, cancellationToken))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetInvestment", new { id = investment.Id }, investment);
        }

        // DELETE: api/Investments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvestment(string id, CancellationToken cancellationToken)
        {
            var investment = await _investmentRepository.Get(id, cancellationToken);

            if (investment == null)
            {
                return NotFound();
            }

            _investmentRepository.Delete(investment);
            await _investmentRepository.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        private bool InvestmentExists(string Id, CancellationToken cancellationToken)
        {
            return _investmentRepository.Find(e => e.Id == Id, cancellationToken)
            .Result
            .Any();
        }
    }
}
