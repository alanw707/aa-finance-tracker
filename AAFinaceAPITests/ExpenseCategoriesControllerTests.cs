using AAExpenseTracker.Domain.Entities;
using AAFinanceTracker.API.Controllers;
using Moq;
using AAFinanceTracker.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace AAFinanceTracker.API.Tests;
public class ExpenseCategoriesControllerTests
{
    private readonly Mock<IRepository<ExpenseCategory>> _expenseCategoriesRepositoryMock = new();

    [Fact]
    public async Task GetExpensesCategories_ShouldReturnAllCategories()
    {
        // Arrange
        var expectedCategories = new List<ExpenseCategory>()
        {
            new() { Name = "Food" },
            new() { Name = "Transportation" }
        };
        _expenseCategoriesRepositoryMock.Setup(x => x.All(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(expectedCategories));

        var controller = new ExpenseCategoriesController(_expenseCategoriesRepositoryMock.Object);

        // Act
        var actualCategories = await controller.GetExpensesCategories(CancellationToken.None);

        // Assert
        _expenseCategoriesRepositoryMock.Verify(x => x.All(It.IsAny<CancellationToken>()), Times.Once);
        Assert.Equal(expectedCategories, actualCategories);
    }

    [Fact]
    public async Task GetExpenseCategory_ExistingName_ReturnsExpenseCategory()
    {
        // Arrange
        var expectedCategory = new ExpenseCategory { Name = "Groceries" };
        _expenseCategoriesRepositoryMock.Setup(x => x.Find(It.IsAny<Expression<Func<ExpenseCategory, bool>>>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(new List<ExpenseCategory>() { expectedCategory }));

        var controller = new ExpenseCategoriesController(_expenseCategoriesRepositoryMock.Object);

        // Act
        var result = await controller.GetExpenseCategory("Groceries", CancellationToken.None);

        // Assert
        _expenseCategoriesRepositoryMock.Verify(x => x.Find(It.IsAny<Expression<Func<ExpenseCategory, bool>>>(), It.IsAny<CancellationToken>()), Times.Once);
        var objectResult = Assert.IsType<ActionResult<ExpenseCategory>>(result);
        Assert.Equal(expectedCategory, objectResult.Value);
    }

    [Fact]
    public async Task PutExpenseCategory_ExistingName_UpdatesAndReturnsNoContent()
    {
        // Arrange
        var existingCategoryName = "Travel";
        var existingCategory = new ExpenseCategory { Name = "Travel" };
        var updatedCategory = new ExpenseCategory { Name = "Business Travel" };

        _expenseCategoriesRepositoryMock.Setup(x 
                => x.Find(It.IsAny<Expression<Func<ExpenseCategory, bool>>>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(new List<ExpenseCategory>() { existingCategory }));

        var controller = new ExpenseCategoriesController(_expenseCategoriesRepositoryMock.Object);

        // Act
        var result = await controller.PutExpenseCategory(existingCategory.Name, updatedCategory, CancellationToken.None);

        // Assert
        _expenseCategoriesRepositoryMock.Verify(x => x.Find(It.IsAny<Expression<Func<ExpenseCategory, bool>>>(), It.IsAny<CancellationToken>()), Times.Once);
        _expenseCategoriesRepositoryMock.Verify(x => x.Update(It.Is<ExpenseCategory>(c => c.Name == updatedCategory.Name)), Times.Once);
        _expenseCategoriesRepositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.IsType<NoContentResult>(result);
    }


}
