
using System.ComponentModel.DataAnnotations.Schema;

namespace AAExpenseTracker.Domain.Entities;

public class Investment
{
    public int Id { get; set; }
    public InvestmentType? Type { get; set; }

    [ForeignKey("TypeName")]
    public string? InvestmentTypeName { get; set; }
    public string? Description { get; set; }
    public decimal InitialInvestment { get; set; }
    public DateTime DateAdded { get; set; }
    public CustodianBank? CustodianBank { get; set; }
    [ForeignKey("Name")]
    public string? CustodianBankName { get; set; } = string.Empty;
}
