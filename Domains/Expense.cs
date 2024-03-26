using aa_finance_tracker.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace aa_finance_tracker.Domains;

public class Expense
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public ExpenseCategory Category { get; set; }
    public ExpenseType Type { get; set; }    
    public decimal Amount { get; set; }
    public string Comments { get; set; }    

    protected Expense()
    {
    }

    public Expense(
        DateTime date,
        ExpenseCategory category,
        ExpenseType type,
        decimal amount,
        string comments)
    {
        Date = date;
        Category = category;
        Type = type;
        Amount = amount;
        Comments = comments;        
    }
}