using AAExpenseTracker.Domain.Entities;

namespace AAFinanceTracker.API.Models;

public class InvestmentModel
{
    public int Id { get; set; }
    public required InvestmentType InvestmentType { get; set; }
    public string? Description { get; set; }
    public decimal InitialInvestment { get; set; }
}