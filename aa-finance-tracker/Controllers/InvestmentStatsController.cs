using AAFinanceTracker.Infrastructure.Repositories.Investment;
using Microsoft.AspNetCore.Mvc;

namespace AAFinanceTracker;

public class InvestmentStatsController : ControllerBase
{
    private readonly IInvestmentRepository _investmentRepo;

    public InvestmentStatsController(IInvestmentRepository investmentRepo)
    {
        _investmentRepo = investmentRepo;
    }
    public async Task<ActionResult> GetInvestmentsByTypeYearMonth(string type, int year, int month, CancellationToken cancellationToken)
    {
        var investments = await _investmentRepo.GetInvestmentsByTypeYearMonth(type, year, month, cancellationToken);

        if (investments.Count == 0)
        {
            return NotFound();
        }

        return Ok(investments);
    }
}
