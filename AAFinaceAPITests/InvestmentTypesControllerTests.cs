using AAExpenseTracker.Domain.Entities;
using AAFinanceTracker.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AAFinanceTracker.Controllers.Tests;

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

}