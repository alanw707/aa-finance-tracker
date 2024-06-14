
using System.ComponentModel.DataAnnotations.Schema;

namespace AAExpenseTracker.Domain.Entities;

public class Investment
{
    public int Id { get; set; }        
    public string? Description { get; set; }
    public decimal InitialInvestment { get; set; }
    public DateTime DateAdded { get; set; }            
    public required string InvestmentTypeName { get; set; }    
    public required int CustodianBankId { get; set; }

    public CustodianBank? CustodianBank { get; set; }
    public InvestmentType? InvestmentType { get; set; }
}
