using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AAExpenseTracker.Domain.Entities;
using AAFinanceTracker.Infrastructure.Repositories;
using AAFinanceTracker.API.Models;
using AAFinanceTracker.Infrastructure.Repositories.Investment;

namespace AAFinanceTracker.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvestmentsController(IServiceProvider _services) : ControllerBase
{
    // GET: api/Investments
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Investment>>> GetInvestments(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
    {
        var investmentRepo = _services.GetRequiredService<IInvestmentRepository>();
        var investments = await investmentRepo.GetInvestmentsTimeframe(startDate, endDate, cancellationToken);

        return Ok(investments);
    }

    // GET: api/Investments/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Investment>> GetInvestment(string id, CancellationToken cancellationToken)
    {
        var investmentRepo = _services.GetRequiredService<IRepository<Investment>>();
        var investment = await investmentRepo.Get(id, cancellationToken);

        if (investment is null)
        {
            return NotFound();
        }

        return Ok(investment);
    }

    // PUT: api/Investments/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutInvestment(int Id, Investment investment, CancellationToken cancellationToken)
    {
        if (Id != investment.Id)
        {
            return BadRequest();
        }

        var investmentRepo = _services.GetRequiredService<IRepository<Investment>>();
        investmentRepo.Update(investment);

        try
        {
            await investmentRepo.SaveChangesAsync(cancellationToken);
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
    [HttpPost]
    public async Task<ActionResult<Investment>> PostInvestment(InvestmentModel investmentModel, [FromServices] IRepository<InvestmentType> _investmentTypesRepsitory, CancellationToken cancellationToken)
    {
        // Check if the investment type already exists
        if (investmentModel is null) return BadRequest("Investment model is null");

        var investment = new Investment()
        {
            DateAdded = DateTime.Now,
            InitialInvestment = investmentModel.InitialInvestment
        };

        if (await _investmentTypesRepsitory.Get(investmentModel.InvestmentType.TypeName, cancellationToken) is not null)
        {
            investment.InvestmentTypeName = investmentModel.InvestmentType.TypeName;
        }
        else
        {
            investment.Type = investmentModel.InvestmentType;
        }

        var investmentRepo = _services.GetRequiredService<IRepository<Investment>>();

        await investmentRepo.Add(investment, cancellationToken);

        try
        {
            await investmentRepo.SaveChangesAsync(cancellationToken);
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

        var investmentRepo = _services.GetRequiredService<IRepository<Investment>>();
        var investment = await investmentRepo.Get(id, cancellationToken);

        if (investment == null)
        {
            return NotFound();
        }

        investmentRepo.Delete(investment);
        await investmentRepo.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    private bool InvestmentExists(int Id, CancellationToken cancellationToken)
    {
        var investmentRepo = _services.GetRequiredService<IRepository<Investment>>();
        return investmentRepo.Find(e => e.Id == Id, cancellationToken)
        .Result
        .Count != 0;
    }
}
