using AAFinanceTracker.Domain.Entities;
using AAFinanceTracker.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AAFinanceTracker.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustodianBanksController(IRepository<CustodianBank> _custodianBankRepository) : ControllerBase
{
    // GET: api/CustodianBanks
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustodianBank>>> GetCustodianBanks(CancellationToken cancellationToken)
    {
        var banks = await _custodianBankRepository.All(cancellationToken);

        if (!banks.Any()) return NotFound();

        return Ok(banks);
    }

    // GET: api/CustodianBanks/5
    [HttpGet("{Id:int}")]
    public async Task<ActionResult<CustodianBank>> GetCustodianBank(int Id, CancellationToken cancellationToken)
    {
        var bank = await _custodianBankRepository.Find(c=>c.Id == Id, cancellationToken);

        if (!bank.Any()) return NotFound();

        return Ok(bank);
    }

    // POST: api/CustodianBanks
    [HttpPost]
    public async Task<ActionResult<CustodianBank>> PostCustodianBank(CustodianBank bank, CancellationToken cancellationToken)
    {
        try
        {
            await _custodianBankRepository.Add(bank, cancellationToken);
            await _custodianBankRepository.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException e)
        {
            if(CustodianBankExists(bank.Id, cancellationToken)) return Conflict();
            throw;
        }

        return CreatedAtAction("GetCustodianBank", new { Id = bank.Id }, bank);
    }

    // PUT: api/CustodianBanks/5
    [HttpPut("{Id}")]
    public async Task<IActionResult> PutCustodianBank(int Id, CustodianBank bank, CancellationToken cancellationToken)
    {
        if (Id != bank.Id) return BadRequest();

        try
        {
            var existingBank = await _custodianBankRepository
                .Find(cb => cb.Id == Id, cancellationToken);

            if (!existingBank.Any())
            {
                return BadRequest();
            }

            _custodianBankRepository.Update(bank);
            await _custodianBankRepository.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            if (!CustodianBankExists(Id, cancellationToken)) return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/CustodianBanks/5
    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteCustodianBank(int Id, CancellationToken cancellationToken)
    {
        var bank = await _custodianBankRepository.Find(c=>c.Id == Id, cancellationToken);

        if (!bank.Any()) return NotFound();

        _custodianBankRepository.Delete(bank.First());
        await _custodianBankRepository.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    private bool CustodianBankExists(int id, CancellationToken cancellationToken)
    {
        return _custodianBankRepository.Find(c=>c.Id == id, cancellationToken).Result.Any();
    }
}
