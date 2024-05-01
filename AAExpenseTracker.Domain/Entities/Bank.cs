namespace AAExpenseTracker.Domain.Entities;

public class Bank
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string? Address { get; set; } = string.Empty;
    public string? Phone { get; set; } = null;

    public Bank(string name)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
    }
}

