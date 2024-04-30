namespace AAExpenseTracker.Domain;

public class Bank
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }

    public Bank(string name)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
    }
}

