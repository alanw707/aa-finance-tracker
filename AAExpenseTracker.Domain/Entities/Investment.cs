
using System.ComponentModel.DataAnnotations.Schema;

namespace AAExpenseTracker.Domain.Entities;

public class Investment
{
    public string Id { get; set; }
    public required InvestmentType Type { get; set; }
    public required Bank Bank { get; set; }
    public string Description { get; set; }
    public decimal InitialInvestment { get; set; }
    public DateTime DateAdded { get; set; }
    public Investment(string description, decimal initialInvestment)
    {
        Id = Guid.NewGuid().ToString();
        Description = description;
        InitialInvestment = initialInvestment;
        DateAdded = DateTime.Now;
    }

}
