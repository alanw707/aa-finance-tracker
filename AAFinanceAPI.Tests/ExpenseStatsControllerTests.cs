using AAFinanceTracker.Controllers;
using AAFinanceTracker.Domain.Entities;
using AAFinanceTracker.Infrastructure.Repositories.Expense;
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
        int month = 5;
        List<Expense> expenses =
        [
            new() { ExpenseCategoryName = categoryName, ExpenseTypeName = "Credit", Amount = 100, Date = new DateTime(year, 10, 15,0,0,0, DateTimeKind.Local) },
            new() { ExpenseCategoryName = categoryName, ExpenseTypeName = "Cash", Amount = 50, Date = new DateTime(year, 10, 20,0,0,0,DateTimeKind.Local) }
        ];

        expenseRepository.Setup(repo => repo.GetExpensesByCategoryYearMonth(categoryName, year, month, CancellationToken.None))
            .ReturnsAsync(expenses);

        // Act
        var result = await expenseStatsController.GetExpensesByCategoryYearMonth(categoryName, year, month, CancellationToken.None);

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

        expenseRepository.Setup(repo => repo.GetExpensesByCategoryYearMonth(category, year, null, CancellationToken.None))
             .ReturnsAsync([]);

        // Act
        var result = await expenseStatsController.GetExpensesByCategoryYearMonth(category, year,null, CancellationToken.None);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
}
