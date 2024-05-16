using AAExpenseTracker.Domain.Entities;
using AAFinanceTracker.API.Controllers;
using AAFinanceTracker.API.Models;
using AAFinanceTracker.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace AAFinanceTracker.API.Tests;

public class ExpensesControllerTests
{
    private Mock<IExpenseRepository> expenseRepositoryMock;
    private Mock<IRepository<Expense>> expenseGenericRepoMock;
    private Mock<IRepository<ExpenseType>> expenseTypesRepoMock;
    private Mock<IRepository<ExpenseCategory>> expenseCategoryRepoMock;
    private ExpensesController expensesController;

    public ExpensesControllerTests()
    {
        expenseRepositoryMock = new Mock<IExpenseRepository>();
        expenseGenericRepoMock = new Mock<IRepository<Expense>>();
        expenseTypesRepoMock = new Mock<IRepository<ExpenseType>>();
        expenseCategoryRepoMock = new Mock<IRepository<ExpenseCategory>>();

        var services = new ServiceCollection();
        services.AddSingleton(expenseRepositoryMock.Object);
        services.AddSingleton(expenseGenericRepoMock.Object);
        services.AddSingleton(expenseTypesRepoMock.Object);
        services.AddSingleton(expenseCategoryRepoMock.Object);
        expensesController = new ExpensesController(services.BuildServiceProvider());
    }

    [Fact]
    public async Task GetExpenses_ValidDateRange_ReturnsOkResult()
    {
        // Arrange
        DateTime startDate = new DateTime(2023, 10, 1);
        DateTime endDate = new DateTime(2023, 10, 31);
        List<Expense> expenses = new List<Expense>()
            {
                new Expense { ExpenseId = 1, Amount = 100, Date = new DateTime(2023, 10, 15), ExpenseCategoryName = "Travel", ExpenseTypeName="Credit Card" },
                new Expense { ExpenseId = 2, Amount = 50, Date = new DateTime(2023, 10, 20), ExpenseCategoryName = "Grocery", ExpenseTypeName="Cash" }
            };
        expenseRepositoryMock.Setup(repo => repo.GetExpensesByTimeframe(startDate, endDate, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expenses);

        // Act
        var result = await expensesController.GetExpenses(startDate, endDate, CancellationToken.None);

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.Equal(expenses, okResult.Value);
    }

    [Fact]
    public async Task GetExpenses_InvalidDateRange_ReturnsNotFoundResult()
    {
        // Arrange
        DateTime startDate = new DateTime(2023, 11, 1);
        DateTime endDate = new DateTime(2023, 11, 30);
        expenseRepositoryMock.Setup(repo => repo.GetExpensesByTimeframe(startDate, endDate, It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        // Act
        var result = await expensesController.GetExpenses(startDate, endDate, CancellationToken.None);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task PostExpense_ValidModel_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var expenseModel = new ExpenseModel()
        {
            CategoryName = "Food",
            TypeName = "Grocery",
            Comments = "Weekly groceries",
            Amount = 100
        };

        var expense = new Expense()
        {
            ExpenseCategoryName = expenseModel.CategoryName,
            ExpenseTypeName = expenseModel.TypeName,
            Comments = expenseModel.Comments,
            Amount = expenseModel.Amount
        };

        expenseTypesRepoMock.Setup(repo => repo.Get(expenseModel.TypeName, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ExpenseType() { Name = expenseModel.TypeName });

        expenseCategoryRepoMock.Setup(repo => repo.Get(expenseModel.CategoryName, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ExpenseCategory() { Name = expenseModel.CategoryName });

        expenseGenericRepoMock.Setup(repo => repo.Add(expense, It.IsAny<CancellationToken>()))
            .ReturnsAsync(It.IsAny<EntityEntry<Expense>>());

        expenseGenericRepoMock.Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Verifiable();

        // Act
        var result = await expensesController.PostExpense(expenseModel, CancellationToken.None);

        // Assert
        expenseGenericRepoMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        var createdAtResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result.Result);
        Assert.NotNull(createdAtResult);
        Assert.Equal(expense.ExpenseTypeName, ((Expense)createdAtResult.Value!).ExpenseTypeName);
        Assert.Equal(expense.ExpenseCategoryName, ((Expense)createdAtResult.Value!).ExpenseCategoryName);
    }

    [Fact]
    public async Task PostExpense_InvalidModel_ReturnsBadRequestResult()
    {
        // Arrange
        ExpenseModel? expenseModel = null;

        // Act
        var result = await expensesController.PostExpense(expenseModel!, CancellationToken.None);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

}

