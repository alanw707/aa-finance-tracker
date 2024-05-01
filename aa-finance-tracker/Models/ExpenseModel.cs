namespace AAFinanceTracker.API.Models;

public class ExpenseModel
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string? Comments { get; set; }
    public required string CategoryName { get; set; }
    public required string TypeName { get; set; }
}