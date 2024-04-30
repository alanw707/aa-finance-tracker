﻿namespace AAExpenseTracker.Domain.Entities;

public class ExpenseType
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }

}
