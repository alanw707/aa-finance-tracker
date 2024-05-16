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
    private ExpensesController expensesController;

    public ExpensesControllerTests()
    {
        expenseRepositoryMock = new Mock<IExpenseRepository>();
        expenseGenericRepoMock = new Mock<IRepository<Expense>>();

        var services = new ServiceCollection();
        services.AddSingleton(expenseRepositoryMock.Object);
        services.AddSingleton(expenseGenericRepoMock.Object);
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

    //     [Fact]
    //     public async Task GetExpenses_InvalidDateRange_ReturnsNotFoundResult()
    //     {
    //         // Arrange
    //         DateTime startDate = new DateTime(2023, 11, 1);
    //         DateTime endDate = new DateTime(2023, 11, 30);
    //         expenseRepositoryMock.Setup(repo => repo.GetExpensesByTimeframe(startDate, endDate, It.IsAny<CancellationToken>()))
    //             .ReturnsAsync((List<Expense>)null);

    //         // Act
    //         var result = await expensesController.GetExpenses(startDate, endDate, CancellationToken.None);

    //         // Assert
    //         Assert.IsType<NotFoundResult>(result);
    //     }
    //     {
    //         // Arrange
    //         var expectedModel = new ExpenseModel()
    //         {
    //             CategoryName = "Travel",
    //             TypeName = "Credit Card",
    //             Comments = "Test comments",
    //             Amount = 10
    //         };

    //     var expense = new Expense()
    //     {
    //         ExpenseCategory = new ExpenseCategory() { Name = expectedModel.CategoryName },
    //         ExpenseType = new ExpenseType() { Name = expectedModel.TypeName },
    //         ExpenseCategoryName = expectedModel.CategoryName,
    //         ExpenseTypeName = expectedModel.TypeName,
    //         Comments = expectedModel.Comments,
    //         Amount = expectedModel.Amount
    //     };

    //     var mockExpenseRepository = new Mock<ExpenseRepository>();
    //     mockExpenseRepository
    //      .Setup(repo => repo.Add(It.IsAny<Expense>(), CancellationToken.None))
    //          .ReturnsAsync(It.IsAny<EntityEntry<Expense>>());

    //     var mockExpenseCategoryRepository = new Mock<IRepository<ExpenseCategory>>();
    //     mockExpenseCategoryRepository.Setup(repo => repo.Get(expectedModel.CategoryName, It.IsAny<CancellationToken>()))
    //         .ReturnsAsync(new ExpenseCategory { Name = expectedModel.CategoryName
    // });

    //         var mockExpenseTypeRepository = new Mock<IRepository<ExpenseType>>();
    // mockExpenseTypeRepository.Setup(repo => repo.Get(expectedModel.TypeName, It.IsAny<CancellationToken>()))
    //         .ReturnsAsync(new ExpenseType { Name = expectedModel.TypeName });

    // var controller = new ExpensesController(mockExpenseRepository.Object);

    // var result = await controller.PostExpense(expectedModel,
    // mockExpenseTypeRepository.Object,
    // mockExpenseCategoryRepository.Object,
    // CancellationToken.None);
    // // Assert        
    // mockExpenseRepository.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

    // var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);

    // Assert.Equal("GetExpense", createdResult.ActionName);
    // Assert.Equal(expense.ExpenseId, createdResult.RouteValues!["expenseId"]);
    // Assert.Equal(expense.ExpenseCategoryName, ((Expense)createdResult.Value!).ExpenseCategoryName);
    // Assert.Equal(expense.ExpenseTypeName, ((Expense)createdResult.Value).ExpenseTypeName);
    // Assert.Equal(expense.ExpenseId, ((Expense)createdResult.Value).ExpenseId);
    //     }
}

