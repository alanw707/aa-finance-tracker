namespace AAFinanceTracker.API.Models;

public class ExpenseModel
{
    public decimal Amount { get; set; }
    public string? Comments { get; set; }
    public required string CategoryName { get; set; }
    public required string TypeName { get; set; }
}
