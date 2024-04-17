using AAExpenseTracker.Domain.Entities;
using AAFinanceTracker.API.Controllers;
using AAFinanceTracker.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AAFinanceTracker.API.Tests;

public class ExpenseTypesControllerTests
{

    private ExpenseTypesController? _controller;

    [Fact]
    public async Task AddExpenseTypesShouldCallExpensesTypeRepository()
    {
        // Arrange
        var expenseTypes = new List<ExpenseType>
        {
            new ("Credit", "Credit Card"),
            new ("Debit","Debit Card")
        };

        var repo = new Mock<IRepository<ExpenseType>>();

        // Inject mocked context into controller
        _controller = new ExpenseTypesController(repo.Object);

        // Act
        var result = await _controller.PostExpenseType(expenseTypes[0], new CancellationToken());

        // Assert
        Assert.NotNull(result);
        repo.Verify(r => r.Add(It.IsAny<ExpenseType>(),It.IsAny<CancellationToken>()),Times.Exactly(1));
    }

    [Fact]
    public async Task GetExpenseTypes_ReturnsListOfExpenseTypes()
    {
        // Arrange
        var expenseTypes = new List<ExpenseType>
        {
            new ("Credit", "Credit Card"),
            new ("Debit","Debit Card")
        };

        var repoMock = new Mock<IRepository<ExpenseType>>();
        repoMock.Setup(r => r.All(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expenseTypes); // Simulate fetching expense types

        var controller = new ExpenseTypesController(repoMock.Object);

        // Act
        var result = await controller.GetExpenseTypes(CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<ActionResult<IEnumerable<ExpenseType>?>>(result);
        var returnedExpenseTypes = Assert.IsAssignableFrom<IEnumerable<ExpenseType>>(okResult.Value);

        Assert.Equal(expenseTypes.Count, returnedExpenseTypes.Count());
    }

    [Fact]
    public async Task PutExpenseType_ValidId_UpdatesExpenseType()
    {
        // Arrange
        var expenseType = new ExpenseType("Travel", "Travel" );

        var repoMock = new Mock<IRepository<ExpenseType>>();

        var validExpenseName = "Travel";

        repoMock.Setup(r => r.Update(expenseType));

        var controller = new ExpenseTypesController(repoMock.Object);

        // Act
        var result = await controller.PutExpenseType(validExpenseName, expenseType, CancellationToken.None);

        // Assert
        Assert.IsType<NoContentResult>(result);
        repoMock.Verify(r => r.Update(expenseType), Times.Once);
        repoMock.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
    [Fact]
    public async Task DeleteExpenseType_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        var id = "nonExistingId"; // Replace with a non-existing ID
        var repoMock = new Mock<IRepository<ExpenseType>>();

        repoMock.Setup(r => r.Find(et=>et.Name==id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<ExpenseType>()); // Simulate not finding any expense type

        var controller = new ExpenseTypesController(repoMock.Object);

        // Act
        var result = await controller.DeleteExpenseType(id, CancellationToken.None);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        repoMock.Verify(r => r.Delete(It.IsAny<ExpenseType>()), Times.Never);
        repoMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}