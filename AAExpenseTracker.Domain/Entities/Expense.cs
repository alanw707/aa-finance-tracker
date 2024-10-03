using System.ComponentModel.DataAnnotations.Schema;

namespace AAFinanceTracker.Domain.Entities;

public class Expense
{
    public int ExpenseId { get; init; }
    public DateTime Date { get; init; }
    [ForeignKey("ExpenseCategoryName")]
    public required string ExpenseCategoryName { get; init; }
    [ForeignKey("ExpenseTypeName")]
    public required string ExpenseTypeName { get; init; }
    public decimal Amount { get; init; }
    public string? Comments { get; init; }
    public ExpenseCategory? ExpenseCategory { get; set; }
    public ExpenseType? ExpenseType { get; set; }

}