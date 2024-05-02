﻿using AAExpenseTracker.Domain.Data;
using AAExpenseTracker.Domain.Entities;

namespace AAFinanceTracker.Infrastructure.Repositories;

public class ExpenseCategoryRepository(FinanceTrackerDbContext context)
    : GenericRepository<ExpenseCategory>(context)
{
}

