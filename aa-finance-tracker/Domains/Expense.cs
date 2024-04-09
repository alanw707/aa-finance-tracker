using aa_finance_tracker.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace aa_finance_tracker.Domains;

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