namespace AAFinanceTracker.API.Models;

public class InvestmentModel
{
    public int Id { get; set; }
    public string? InvestmentTypeName { get; set; }
    public string? Description { get; set; }
    public decimal InitialInvestment { get; set; }
}