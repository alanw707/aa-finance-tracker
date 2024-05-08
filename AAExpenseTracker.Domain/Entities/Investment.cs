
using System.ComponentModel.DataAnnotations.Schema;

namespace AAExpenseTracker.Domain.Entities;

public class Investment
{
    public required string Id { get; set; }
    public InvestmentType? Type { get; set; }
    [ForeignKey("TypeName")]
    public string? InvestmentTypeName { get; set; }
    public Bank? Bank { get; set; }
    [ForeignKey("BankId")]
    public string? BankId { get; set; }
    public string? Description { get; set; }
    public decimal InitialInvestment { get; set; }
    public DateTime DateAdded { get; set; }   
}
