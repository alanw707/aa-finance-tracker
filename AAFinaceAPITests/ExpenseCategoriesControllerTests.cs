using System.Linq.Expressions;
using AAExpenseTracker.Domain.Entities;
using AAFinanceTracker.API.Controllers;
using AAFinanceTracker.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

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
            .ReturnsAsync(expectedCategories);

        var controller = new ExpenseCategoriesController(_expenseCategoriesRepositoryMock.Object);

        // Act
        var returnedResult = await controller.GetExpensesCategories(CancellationToken.None);

        // Assert
        _expenseCategoriesRepositoryMock.Verify(x => x.All(It.IsAny<CancellationToken>()), Times.Once);

        var okResult = Assert.IsType<OkObjectResult>(returnedResult.Result);
        var actualInvestmentsTypes = Assert.IsAssignableFrom<IEnumerable<ExpenseCategory>>(okResult.Value);
        Assert.Equal(expectedCategories, actualInvestmentsTypes);
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

    [Fact]
    public async Task PostExpenseCategory_ValidCategory_ReturnsCreatedResponse()
    {
        // Arrange
        var newCategory = new ExpenseCategory { Name = "Utilities" };


        var controller = new ExpenseCategoriesController(_expenseCategoriesRepositoryMock.Object);

        // Act
        var result = await controller.PostExpenseCategory(newCategory, CancellationToken.None);

        // Assert
        _expenseCategoriesRepositoryMock.Verify(x => x.Add(newCategory, It.IsAny<CancellationToken>()), Times.Once);
        _expenseCategoriesRepositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal("GetExpenseCategory", createdResult.ActionName);
        Assert.Equal(newCategory.Name, createdResult.RouteValues?["name"]);
        Assert.Equal(newCategory, createdResult.Value);
    }

    [Fact]
    public async Task DeleteExpenseCategory_ReturnsNoContent_WhenCategoryExistsAndIsDeleted()
    {
        // Arrange
        var existingCategory = new ExpenseCategory { Name = "Office Supplies" };
        _expenseCategoriesRepositoryMock.Setup(x => x.Find(It.IsAny<Expression<Func<ExpenseCategory, bool>>>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(new List<ExpenseCategory>() { existingCategory }));
        _expenseCategoriesRepositoryMock.Setup(x => x.Delete(existingCategory))
            .Verifiable(Times.Once);
        _expenseCategoriesRepositoryMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(1));

        var controller = new ExpenseCategoriesController(_expenseCategoriesRepositoryMock.Object);

        // Act
        var result = await controller.DeleteExpenseCategory(existingCategory.Name, CancellationToken.None);

        // Assert
        _expenseCategoriesRepositoryMock.Verify(x => x.Find(It.IsAny<Expression<Func<ExpenseCategory, bool>>>(), It.IsAny<CancellationToken>()), Times.Once);
        _expenseCategoriesRepositoryMock.Verify(x => x.Delete(existingCategory), Times.Once);
        _expenseCategoriesRepositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.IsType<NoContentResult>(result);
    }
}
