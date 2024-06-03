namespace AAExpenseTracker.Domain.Entities;

public class CustodianBank
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public decimal Balance { get; set; }
}
