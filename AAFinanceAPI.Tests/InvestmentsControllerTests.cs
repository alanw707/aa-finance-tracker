using AAExpenseTracker.Domain.Entities;
using AAFinanceTracker.API.Models;
using AAFinanceTracker.Controllers;
using AAFinanceTracker.Infrastructure.Repositories;
using AAFinanceTracker.Infrastructure.Repositories.Investment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace AAFinanceTracker.API.Tests;

public class InvestmentsControllerTests
{

    private readonly Mock<IInvestmentRepository> investmentRepositoryMock;
    private readonly Mock<IRepository<Investment>> investmentGenericRepoMock;
    private readonly Mock<IRepository<InvestmentType>> investmentTypeRepoMock;
    private readonly InvestmentsController controller;
    public InvestmentsControllerTests()
    {
        investmentRepositoryMock = new Mock<IInvestmentRepository>();
        investmentGenericRepoMock = new Mock<IRepository<Investment>>();
        investmentTypeRepoMock = new Mock<IRepository<InvestmentType>>();

        var services = new ServiceCollection();
        services.AddSingleton(investmentRepositoryMock.Object);
        services.AddSingleton(investmentGenericRepoMock.Object);
        services.AddSingleton(investmentTypeRepoMock.Object);
        controller = new InvestmentsController(services.BuildServiceProvider());
    }
    // Test the GetAll method of the InvestmentController class
    [Fact]
    public async Task GetInvestments_ShouldReturnAllInvestments()
    {
        // Arrange        
        DateTime startDate = new(2023, 10, 1);
        DateTime endDate = new(2023, 10, 31);
        var investments = new List<Investment>()
                {
                    new() { Id = 1, CustodianBankId = 1, InvestmentTypeName = "Stocks", InitialInvestment = 1000, DateAdded = new DateTime(2023, 10, 15) },
                    new() { Id = 2, CustodianBankId = 2, InvestmentTypeName = "Bonds", InitialInvestment = 500, DateAdded = new DateTime(2023, 10, 20) }
                };        

        investmentRepositoryMock.Setup(repo => repo.GetInvestmentsTimeframe(startDate, endDate, It.IsAny<CancellationToken>()))
            .ReturnsAsync(investments);
        // Act        
        var result = await controller.GetInvestments(startDate, endDate, CancellationToken.None);

        // Assert        
        Assert.IsType<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.Equal(investments, okResult?.Value);
    }

    // Test the GetById method of the InvestmentController class
    [Fact]
    public async Task GetInvestment_ShouldReturnTheInvestmentWithTheGivenId()
    {
        // Arrange        
        var investment = new Investment() { Id = 1, CustodianBankId = 1, InvestmentTypeName = "Stock", InitialInvestment = 500 };
    
        investmentGenericRepoMock.Setup(repo => repo.Get("1", It.IsAny<CancellationToken>()))
        .ReturnsAsync(investment);
        
        // Act
        // Call the GetById method with the id of the investment we want to find
        var result = await controller.GetInvestment("1", It.IsAny<CancellationToken>());

        // Assert
        // Assert that the result is a successful OkObjectResult
        Assert.IsType<OkObjectResult>(result.Result);

        // Assert that the result contains the correct investment
        var okResult = result.Result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.Equal(investment, okResult.Value);
    }

    // // Test the Create method of the InvestmentController class
    [Fact]
    public async Task Post_ShouldCreateANewInvestment()
    {
        // Arrange
        var investmentType = new InvestmentType() { Name = "Stock" };
        var investmentModel = new InvestmentModel() { InvestmentType = investmentType, InitialInvestment = 500 };

        var investment = new Investment() { CustodianBankId=1, InvestmentType= investmentType, InvestmentTypeName = investmentType.Name, InitialInvestment = 500 };        

        investmentTypeRepoMock.Setup(repo => repo.Get(investmentType.Name, It.IsAny<CancellationToken>()))
            .ReturnsAsync(investmentType);
        investmentGenericRepoMock.Setup(repo => repo.Add(investment, It.IsAny<CancellationToken>()))
            .ReturnsAsync(It.IsAny<EntityEntry<Investment>>());                

        // Act                
        var result = await controller.PostInvestment(investmentModel, investmentTypeRepoMock.Object, It.IsAny<CancellationToken>());

        // Assert        
        // Assert that the result is a successful CreatedAtActionResult
        Assert.IsType<CreatedAtActionResult>(result.Result);

        // Assert that the result contains the correct investment
        var createdAtActionResult = result.Result as CreatedAtActionResult;
        Assert.NotNull(createdAtActionResult);
        Assert.Equal(investment.InvestmentType.Name, ((Investment)createdAtActionResult.Value!).InvestmentTypeName);
    }

    // // Test the Update method of the InvestmentController class
    [Fact]
    public async Task Update_ShouldUpdateTheInvestmentWithTheGivenId()
    {
        // Arrange                
        var investment = new Investment { Id = 1, CustodianBankId = 1, InvestmentTypeName = "Stock", InitialInvestment = 500 };
        // Corrected the repository type to IRepository<Investment>        

        investmentGenericRepoMock.Setup(repo => repo.Update(It.IsAny<Investment>()))
        .Returns(investment);

        // Act                
        var result = await controller.PutInvestment(investment.Id, investment, CancellationToken.None);
        // Assert                
        Assert.IsType<NoContentResult>(result);
    }

}