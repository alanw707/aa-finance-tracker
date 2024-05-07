using AAExpenseTracker.Domain.Entities;
using AAFinanceTracker.Controllers;
using AAFinanceTracker.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
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
                    new Investment (new InvestmentType(){ TypeName = "Stocks"},1000),
                    new Investment (new InvestmentType(){ TypeName = "Bonds"},500)
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
    // [Fact]
    // public async Task GetById_ShouldReturnTheInvestmentWithTheGivenId()
    // {
    //     // Arrange
    //     // Create a mock repository for investments
    //     var investment = new Investment { Id = 1, Name = "Stock XYZ", AmountInvested = 1000 };
    //     var mockInvestmentRepository = new Mock<IInvestmentRepository>();
    //     mockInvestmentRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(investment);

    //     // Create an instance of the InvestmentController
    //     var controller = new InvestmentController(mockInvestmentRepository.Object);

    //     // Act
    //     // Call the GetById method with the id of the investment we want to find
    //     var result = await controller.GetById(1);

    //     // Assert
    //     // Assert that the result is a successful OkObjectResult
    //     Assert.IsType<OkObjectResult>(result);

    //     // Assert that the result contains the correct investment
    //     var okResult = result as OkObjectResult;
    //     Assert.Equal(investment, okResult.Value);
    // }

    // // Test the Create method of the InvestmentController class
    // [Fact]
    // public async Task Create_ShouldCreateANewInvestment()
    // {
    //     // Arrange
    //     // Create a mock repository for investments
    //     var investment = new Investment { Name = "Stock XYZ", AmountInvested = 1000 };
    //     var mockInvestmentRepository = new Mock<IInvestmentRepository>();
    //     mockInvestmentRepository.Setup(repo => repo.Create(investment)).ReturnsAsync(investment);

    //     // Create an instance of the InvestmentController
    //     var controller = new InvestmentController(mockInvestmentRepository.Object);

    //     // Act
    //     // Call the Create method with the investment we want to create
    //     var result = await controller.Create(investment);

    //     // Assert
    //     // Assert that the result is a successful CreatedAtActionResult
    //     Assert.IsType<CreatedAtActionResult>(result);

    //     // Assert that the result contains the correct investment
    //     var createdAtActionResult = result as CreatedAtActionResult;
    //     Assert.Equal(investment, createdAtActionResult.Value);
    // }

    // // Test the Update method of the InvestmentController class
    // [Fact]
    // public async Task Update_ShouldUpdateTheInvestmentWithTheGivenId()
    // {
    //     // Arrange
    //     // Create a mock repository for investments
    //     var investment = new Investment { Id = 1, Name = "Stock XYZ", AmountInvested = 1000 };
    //     var mockInvestmentRepository = new Mock<IInvestmentRepository>();
    //     mockInvestmentRepository.Setup(repo => repo.Update(investment)).ReturnsAsync(investment);

    //     // Create an instance of the InvestmentController
    //     var controller = new InvestmentController(mockInvestmentRepository.Object);

    //     // Act
    //     // Call the Update method with the investment we want to update
    //     var result = await controller.Update(investment.Id, investment);

    //     // Assert
    //     // Assert that the result is a successful NoContentResult
    //     Assert.IsType<NoContentResult>(result);
    // }
}