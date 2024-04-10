using System.ComponentModel.DataAnnotations.Schema;

namespace AAExpenseTracker.Domain.Entities;

public class Expense
{
    public int ExpenseId { get; set; }
    public DateTime Date { get; set; }
    [ForeignKey("ExpenseCategoryName")]
    public string ExpenseCategoryName { get; set; }
    [ForeignKey("ExpenseTypeName")]
    public string ExpenseTypeName { get; set; }    
    public decimal Amount { get; set; }
    public string? Comments { get; set; }
    public ExpenseCategory ExpenseCategory { get; set; }
    public ExpenseType ExpenseType { get; set; }

}