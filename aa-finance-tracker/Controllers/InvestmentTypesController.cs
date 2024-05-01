using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AAExpenseTracker.Domain.Entities;
using AAFinanceTracker.Infrastructure.Repositories;

namespace AAFinanceTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentTypesController(IRepository<InvestmentType> _investmentTypeRepository) : ControllerBase
    {
        // GET: api/InvestmentTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvestmentType>>> GetInvestmentsTypes(CancellationToken cancellationToken)
        {
            return await _investmentTypeRepository.All(cancellationToken);
        }

        // GET: api/InvestmentTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InvestmentType>> GetInvestmentType(string id, CancellationToken cancellationToken)
        {
            var investmentType = await _investmentTypeRepository.Get(id, cancellationToken);

            if (investmentType == null)
            {
                return NotFound();
            }

            return investmentType;
        }

        // PUT: api/InvestmentTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{type}")]
        public async Task<IActionResult> PutInvestmentType(string typeName, InvestmentType investmentType, CancellationToken cancellationToken)
        {
            // if (typeName != investmentType.Type)
            // {
            //     return BadRequest();
            // }

            _investmentTypeRepository.Update(investmentType);

            try
            {
                await _investmentTypeRepository.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                // if (!InvestmentTypeExists(typeName, cancellationToken))
                // {
                //     return NotFound();
                // }
                // else
                // {
                //     throw;
                // }
            }

            return NoContent();
        }

        // POST: api/InvestmentTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InvestmentType>> PostInvestmentType(InvestmentType investmentType, CancellationToken cancellationToken)
        {
            await _investmentTypeRepository.Add(investmentType, cancellationToken);

            try
            {
                await _investmentTypeRepository.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException)
            {
                // if (InvestmentTypeExists(investmentType.Type, cancellationToken))
                // {
                //     return Conflict();
                // }
                // else
                // {
                //     throw;
                // }
            }
            return NoContent();
            // return CreatedAtAction("GetInvestmentType", new { id = investmentType.Type }, investmentType);
        }

        // DELETE: api/InvestmentTypes/5
        [HttpDelete("{typeName}")]
        public async Task<IActionResult> DeleteInvestmentType(string typeName, CancellationToken cancellationToken)
        {
            var investmentType = await _investmentTypeRepository.Get(typeName, cancellationToken);

            if (investmentType is null)
            {
                return NotFound();
            }

            _investmentTypeRepository.Delete(investmentType);
            await _investmentTypeRepository.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        // private bool InvestmentTypeExists(string typeName, CancellationToken cancellationToken)
        // {
        //     return _investmentTypeRepository.Find(e => e.Type == typeName, cancellationToken)
        //     .Result.Any();
        // }
    }
}

