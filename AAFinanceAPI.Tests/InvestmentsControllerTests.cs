using AAExpenseTracker.Domain.Entities;
using AAFinanceTracker.API.Models;
using AAFinanceTracker.Controllers;
using AAFinanceTracker.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;

namespace AAFinanceTracker.API.Tests;

public class InvestmentsControllerTests
{
    // Test the GetAll method of the InvestmentController class
    [Fact]
    public async Task GetInvestments_ShouldReturnAllInvestments()
    {
        // Arrange        
        var investments = new List<Investment>()
                {
                    new() { Id = 1, InvestmentTypeName = "Stocks", InitialInvestment = 1000 },
                    new() { Id = 2, InvestmentTypeName = "Bonds", InitialInvestment = 500 }
                };

        var mockInvestmentRepository = new Mock<IRepository<Investment>>();

        mockInvestmentRepository.Setup(repo => repo.All(It.IsAny<CancellationToken>()))
            .ReturnsAsync(investments);


        var controller = new InvestmentsController(mockInvestmentRepository.Object);

        // Act        
        var result = await controller.GetInvestments(CancellationToken.None);

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
        var investment = new Investment() { Id = 1, InvestmentTypeName = "Stock", InitialInvestment = 500 };

        var mockInvestmentRepository = new Mock<IRepository<Investment>>();

        mockInvestmentRepository.Setup(repo => repo.Get("1", It.IsAny<CancellationToken>()))
        .ReturnsAsync(investment);

        var controller = new InvestmentsController(mockInvestmentRepository.Object);

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
        var investmentType = new InvestmentType() { TypeName = "Stock" };
        var investmentModel = new InvestmentModel() { InvestmentType = investmentType, InitialInvestment = 500 };

        var investment = new Investment() { Type = investmentType, InitialInvestment = 500 };

        var mockInvestmentRepository = new Mock<IRepository<Investment>>();
        var mockInvestmentTypeRepository = new Mock<IRepository<InvestmentType>>();

        mockInvestmentTypeRepository.Setup(repo => repo.Get(investmentType.TypeName, It.IsAny<CancellationToken>()))
            .ReturnsAsync(investmentType);
        mockInvestmentRepository.Setup(repo => repo.Add(investment, It.IsAny<CancellationToken>()))
            .ReturnsAsync(It.IsAny<EntityEntry<Investment>>());

        // Create an instance of the InvestmentController
        var controller = new InvestmentsController(mockInvestmentRepository.Object);

        // Act                
        var result = await controller.PostInvestment(investmentModel, mockInvestmentTypeRepository.Object, It.IsAny<CancellationToken>());

        // Assert        
        // Assert that the result is a successful CreatedAtActionResult
        Assert.IsType<CreatedAtActionResult>(result.Result);

        // Assert that the result contains the correct investment
        var createdAtActionResult = result.Result as CreatedAtActionResult;
        Assert.NotNull(createdAtActionResult);
        Assert.Equal(investment.Type.TypeName, ((Investment)createdAtActionResult.Value!).InvestmentTypeName);
    }

    // // Test the Update method of the InvestmentController class
    [Fact]
    public async Task Update_ShouldUpdateTheInvestmentWithTheGivenId()
    {
        // Arrange                
        var investment = new Investment { Id = 1, InvestmentTypeName = "Stock", InitialInvestment = 500 };
        var mockInvestmentRepository = new Mock<IRepository<Investment>>();        // Corrected the repository type to IRepository<Investment>        
        mockInvestmentRepository.Setup(repo => repo.Update(It.IsAny<Investment>()))
        .Returns(investment);

        var controller = new InvestmentsController(mockInvestmentRepository.Object);        // Corrected the controller type to InvestmentsController                
        // Act                
        var result = await controller.PutInvestment(investment.Id, investment, CancellationToken.None);
        // Assert                
        Assert.IsType<NoContentResult>(result);
    }

}