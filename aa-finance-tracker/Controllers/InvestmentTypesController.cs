using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AAExpenseTracker.Domain.Entities;
using AAFinanceTracker.Infrastructure.Repositories;

namespace AAFinanceTracker.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvestmentTypesController(IRepository<InvestmentType> _investmentTypeRepository) : ControllerBase
{
    // GET: api/InvestmentTypes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<InvestmentType>>> GetInvestmentsTypes(CancellationToken cancellationToken)
    {
        var result = await _investmentTypeRepository.All(cancellationToken);

        if (result.Any())
        {
            return Ok(result);
        }
        else
        {
            return NotFound();
        }
    }

    // GET: api/InvestmentTypes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<InvestmentType>> GetInvestmentType(string id, CancellationToken cancellationToken)
    {
        var investmentType = await _investmentTypeRepository.Get(id, cancellationToken);

        if (investmentType is null)
        {
            return NotFound();
        }

        return Ok(investmentType);
    }

    // PUT: api/InvestmentTypes/5
    // To protect from over-posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{typeName}")]
    public async Task<IActionResult> PutInvestmentType(string typeName, InvestmentType investmentType, CancellationToken cancellationToken)
    {
        if (typeName != investmentType.TypeName)
        {
            return BadRequest();
        }

        _investmentTypeRepository.Update(investmentType);

        try
        {
            await _investmentTypeRepository.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!InvestmentTypeExists(typeName, cancellationToken))
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
            if (InvestmentTypeExists(investmentType.TypeName, cancellationToken))
            {
                return Conflict();
            }
            else
            {
                throw;
            }
        }
        return CreatedAtAction("GetInvestmentType", new { id = investmentType.TypeName }, investmentType);
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

    private bool InvestmentTypeExists(string typeName, CancellationToken cancellationToken)
    {
        var foundTypes = _investmentTypeRepository.Find(e => e.TypeName == typeName, cancellationToken);

        if (foundTypes.Result is null || foundTypes.Result.Count == 0)
        {
            return false;
        }

        return true;
    }
}

