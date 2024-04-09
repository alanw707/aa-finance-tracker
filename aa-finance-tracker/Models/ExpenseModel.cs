using aa_finance_tracker.Data;
using aa_finance_tracker.Domains;
using System.ComponentModel.DataAnnotations.Schema;

namespace aa_finance_tracker.Models;

public class ExpenseModel
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string Comments { get; set; }
    public string CategoryName { get; set; }
    public string TypeName { get; set; }
}