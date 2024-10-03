namespace AAFinanceTracker.Domain.Entities;

public record ExpenseCategory
{
    public required string Name { get; init; }
    public required string? Description { get; init; }
    public decimal Budget { get; init; }
    public DateTime CreatedDate { get; init; }
    public DateTime UpdatedDate { get; init; }
}