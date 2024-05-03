using AAExpenseTracker.Domain.Entities;
using AAFinanceTracker.Controllers;
using AAFinanceTracker.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace AAFinanceTracker.API.Tests;

public class InvestmentsTypesControllerTests
{
    [Fact]
    public async Task GetInvestmentsTypes_ReturnsOk_WhenDatabaseHasData()
    {
        // Arrange
        var expectedInvestmentTypes = new List<InvestmentType>()
        {
            new InvestmentType { TypeName = "Stocks" },
            new InvestmentType { TypeName = "Bonds" }
        };

        var mockRepository = new Mock<IRepository<InvestmentType>>();
        mockRepository.Setup(repo => repo.All(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedInvestmentTypes);
        var controller = new InvestmentTypesController(mockRepository.Object);

        // Act
        var result = await controller.GetInvestmentsTypes(CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var investmentsTypes = Assert.IsAssignableFrom<IEnumerable<InvestmentType>>(okResult.Value);
        Assert.Equal(2, investmentsTypes.Count());
    }

    [Fact]
    public async Task GetInvestmentsTypes_ReturnsNotFound_WhenDatabaseIsEmpty()
    {
        // Arrange
        var mockRepository = new Mock<IRepository<InvestmentType>>();
        mockRepository.Setup(repo => repo.All(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Enumerable.Empty<InvestmentType>());

        var controller = new InvestmentTypesController(mockRepository.Object);

        // Act
        var result = await controller.GetInvestmentsTypes(CancellationToken.None);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetInvestmentType_ReturnsOk_WhenInvestmentTypeExists()
    {
        // Arrange
        var investmentType = new InvestmentType { TypeName = "Bonds" };
        var investmentTypeRepositoryMock = new Mock<IRepository<InvestmentType>>();
        investmentTypeRepositoryMock.Setup(r => r.Get("Bonds", It.IsAny<CancellationToken>()))
            .ReturnsAsync(investmentType);

        var controller = new InvestmentTypesController(investmentTypeRepositoryMock.Object);
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await controller.GetInvestmentType("Bonds", cancellationToken);

        // Assert
        Assert.NotNull(result);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(200, okResult.StatusCode);
        var returnedInvestmentType = okResult.Value;
        Assert.Equal(investmentType, returnedInvestmentType);
    }

    [Fact]
    public async Task PutInvestmentType_ReturnsNoContent_WhenInvestmentTypeExistsAndIsUpdated()
    {
        // Arrange        
        var updatedInvestmentType = new InvestmentType { TypeName = "Bonds" };
        var investmentTypeRepositoryMock = new Mock<IRepository<InvestmentType>>();

        investmentTypeRepositoryMock.Setup(x => x.Update(updatedInvestmentType))
            .Verifiable(Times.Once);
        investmentTypeRepositoryMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(1));

        var controller = new InvestmentTypesController(investmentTypeRepositoryMock.Object);

        // Act
        var result = await controller.PutInvestmentType(updatedInvestmentType.TypeName, updatedInvestmentType, CancellationToken.None);

        // Assert        
        investmentTypeRepositoryMock.Verify(x => x.Update(updatedInvestmentType), Times.Once);
        investmentTypeRepositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task PutInvestmentType_ReturnsNotFound_WhenInvestmentTypeDoesNotExist()
    {
        // Arrange
        var nonExistingInvestmentType = new InvestmentType { TypeName = "Bonds" };

        var investmentTypeRepositoryMock = new Mock<IRepository<InvestmentType>>();

        investmentTypeRepositoryMock.Setup(x => x.Update(nonExistingInvestmentType))
            .Returns((InvestmentType)null!);
        investmentTypeRepositoryMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
        .ThrowsAsync(new DbUpdateConcurrencyException());

        var controller = new InvestmentTypesController(investmentTypeRepositoryMock.Object);

        // Act
        var result = await controller.PutInvestmentType(nonExistingInvestmentType.TypeName, nonExistingInvestmentType, CancellationToken.None);

        // Assert        
        investmentTypeRepositoryMock.Verify(x => x.Update(nonExistingInvestmentType), Times.Once);
        investmentTypeRepositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task PutInvestmentType_ReturnsBadRequest_WhenIdInRequestBodyDoesntMatchIdInUrl()
    {
        // Arrange
        var existingInvestmentType = new InvestmentType { TypeName = "Stocks" };
        var updatedInvestmentType = new InvestmentType { TypeName = "Bonds" };

        var investmentTypeRepositoryMock = new Mock<IRepository<InvestmentType>>();

        investmentTypeRepositoryMock.Setup(x => x.Get(existingInvestmentType.TypeName, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingInvestmentType);

        var controller = new InvestmentTypesController(investmentTypeRepositoryMock.Object);

        // Act
        var result = await controller.PutInvestmentType(existingInvestmentType.TypeName, updatedInvestmentType, CancellationToken.None);

        // Assert
        investmentTypeRepositoryMock.Verify(x => x.Get(existingInvestmentType.TypeName, It.IsAny<CancellationToken>()), Times.Never);
        investmentTypeRepositoryMock.Verify(x => x.Update(updatedInvestmentType), Times.Never);
        investmentTypeRepositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        Assert.IsType<BadRequestResult>(result);
    }

}