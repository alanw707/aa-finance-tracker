
using System.ComponentModel.DataAnnotations.Schema;

namespace AAExpenseTracker.Domain.Entities;

public class Investment
{
    public string Id { get; set; }
    public InvestmentType? Type { get; set; }
    [ForeignKey("TypeName")]
    public string? InvestmentTypeName { get; set; }
    public Bank? Bank { get; set; }
    [ForeignKey("BankId")]
    public string? BankId { get; set; }
    public string? Description { get; set; }
    public decimal InitialInvestment { get; set; }
    public DateTime DateAdded { get; set; }
    public Investment(InvestmentType type, decimal initialInvestment)
    {
        Id = Guid.NewGuid().ToString();
        Type = type;
        InitialInvestment = initialInvestment;
        DateAdded = DateTime.Now;
    }

}
