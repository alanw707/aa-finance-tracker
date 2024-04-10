namespace AAExpenseTracker.Domain.Entities;

public class ExpenseCategory
{

    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Budget { get; set; }
    public string ColourHex { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}