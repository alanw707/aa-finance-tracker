using AAExpenseTracker.Domain.Entities;
using AAFinanceTracker.Infrastructure.Repositories;
using AAFinanceTracker.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AAFinanceTracker.API.Tests;

public class ExpenseStatsControllerTests
{
    private readonly ExpenseStatsController expenseStatsController;
    private readonly Mock<IExpenseRepository> expenseRepository;


    public ExpenseStatsControllerTests()
    {
        expenseRepository = new Mock<IExpenseRepository>();
        expenseStatsController = new ExpenseStatsController(expenseRepository.Object);

    }

    [Fact]
    public async Task GetExpensesByCategoryYear_ValidCategoryAndYear_ReturnsOkResult()
    {
        // Arrange
        string categoryName = "Food";
        int year = 2023;
        List<Expense> expenses = new List<Expense>()
        {
            new Expense { ExpenseCategoryName = categoryName, ExpenseTypeName = "Credit", Amount = 100, Date = new DateTime(year, 10, 15) },
            new Expense { ExpenseCategoryName = categoryName, ExpenseTypeName = "Cash", Amount = 50, Date = new DateTime(year, 10, 20) }
        };

        expenseRepository.Setup(repo => repo.GetExpensesByCategoryYear(categoryName, year, CancellationToken.None))
            .ReturnsAsync(expenses);

        // Act
        var result = await expenseStatsController.GetExpensesByCategoryYear(categoryName, year, CancellationToken.None);

        // Assert        
        Assert.NotNull(result);
        var okResult = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
        Assert.Equal(expenses, okResult.Value);
    }

    [Fact]
    public async Task GetExpensesByCategoryYear_InvalidCategory_ReturnsNotFoundResult()
    {
        // Arrange
        string category = "InvalidCategory";
        int year = 2023;

        expenseRepository.Setup(repo => repo.GetExpensesByCategoryYear(category, year, CancellationToken.None))
             .ReturnsAsync(new List<Expense>());

        // Act
        var result = await expenseStatsController.GetExpensesByCategoryYear(category, year, CancellationToken.None);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
}
