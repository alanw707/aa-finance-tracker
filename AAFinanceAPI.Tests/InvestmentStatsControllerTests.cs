using AAFinanceTracker.Controllers;
using AAFinanceTracker.Domain.Entities;
using AAFinanceTracker.Infrastructure.Repositories.Investment;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AAFinanceTracker.API.Tests;

public class InvestmentStatsControllerTests
{
    
    private readonly InvestmentStatsController investmentStatsController;
    private readonly Mock<IInvestmentRepository> investmentRepository;


    public InvestmentStatsControllerTests()
    {
        investmentRepository = new Mock<IInvestmentRepository>();
        investmentStatsController = new InvestmentStatsController(investmentRepository.Object);

    }

    [Fact]
    public async Task GetExpensesByCategoryYear_ValidCategoryAndYear_ReturnsOkResult()
    {
        // Arrange
        string investmentType = "Stocks";
        int year = 2023;
        int month = 5;
        List<Investment> investments =
        [
            new() { InvestmentTypeName = investmentType, InitialInvestment = 10000, CustodianBankId = 1 },
            new() { InvestmentTypeName = investmentType, InitialInvestment = 20000, CustodianBankId = 2 },
        ];

        investmentRepository.Setup(repo => repo.GetInvestmentsByTypeYearMonth(investmentType, year, month, CancellationToken.None))
            .ReturnsAsync(investments);

        // Act
        var result = await investmentStatsController.GetInvestmentsByTypeYearMonth(investmentType, year, month, CancellationToken.None);

        // Assert        
        Assert.NotNull(result);
        var okResult = Assert.IsAssignableFrom<OkObjectResult>(result);
        Assert.Equal(investments, okResult.Value);
    }

}
