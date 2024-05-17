namespace AAFinanceTracker;

public class ExpenseCategoryStatsModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public decimal Budget { get; set; }
    public decimal Spent { get; set; }
    public int Month { get; set; }

}
