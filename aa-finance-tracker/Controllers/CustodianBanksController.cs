using AAFinanceTracker.Domain.Entities;
using AAFinanceTracker.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustodianBanksController : ControllerBase
{
    private readonly IRepository<CustodianBank> _custodianBankRepository;

    public CustodianBanksController(IRepository<CustodianBank> _custodianBankRepository)
    {
        this._custodianBankRepository = _custodianBankRepository;
    }

    // GET: api/CustodianBanks
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustodianBank>>> GetCustodianBanks(CancellationToken cancellationToken)
    {
        var banks = await _custodianBankRepository.All(cancellationToken);

        if (!banks.Any()) return NotFound();

        return Ok(banks);
    }

    // GET: api/CustodianBanks/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CustodianBank>> GetCustodianBank(int id, CancellationToken cancellationToken)
    {
        var bank = await _custodianBankRepository.Find(c=>c.Id == id, cancellationToken);

        if (bank == null) return NotFound();

        return Ok(bank);
    }

    // POST: api/CustodianBanks
    [HttpPost]
    public async Task<ActionResult<CustodianBank>> PostCustodianBank(CustodianBank bank, CancellationToken cancellationToken)
    {
        await _custodianBankRepository.Add(bank, cancellationToken);
        await _custodianBankRepository.SaveChangesAsync(cancellationToken);

        return CreatedAtAction("GetCustodianBank", new { id = bank.Id }, bank);
    }

    // PUT: api/CustodianBanks/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCustodianBank(int id, CustodianBank bank, CancellationToken cancellationToken)
    {
        if (id != bank.Id) return BadRequest();

        try
        {
            _custodianBankRepository.Update(bank);
            await _custodianBankRepository.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            if (!CustodianBankExists(id, cancellationToken)) return NotFound();
            else throw;
        }

        return NoContent();
    }

    // DELETE: api/CustodianBanks/5
    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteCustodianBank(int Id, CancellationToken cancellationToken)
    {
        var bank = await _custodianBankRepository.Find(c=>c.Id == Id, cancellationToken);

        if (bank == null) return NotFound();

        _custodianBankRepository.Delete(bank.First());
        await _custodianBankRepository.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    private bool CustodianBankExists(int id, CancellationToken cancellationToken)
    {
        return _custodianBankRepository.Find(c=>c.Id == id, cancellationToken).Result.Any();
    }
}
