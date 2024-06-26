using System.Linq.Expressions;
using AAFinanceTracker.Domain.Entities;
using AAFinanceTracker.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using YourNamespace.API.Controllers;

namespace AAFinanceTracker.API.Tests
{
    public class CustodianBanksControllerTests
    {
        private readonly Mock<IRepository<CustodianBank>> _mockRepository;
        private readonly CustodianBanksController _controller;

        public CustodianBanksControllerTests()
        {
            _mockRepository = new Mock<IRepository<CustodianBank>>();
            _controller = new CustodianBanksController(_mockRepository.Object);
        }

        [Fact]
        public async Task GetCustodianBanks_ReturnsOkResult_WhenBanksExist()
        {
            // Arrange
            var banks = new List<CustodianBank>
                {
                    new CustodianBank { Id = 1, Name = "Bank 1" },
                    new CustodianBank { Id = 2, Name = "Bank 2" }
                };
            _mockRepository.Setup(repo => repo.All(default)).ReturnsAsync(banks);

            // Act
            var result = await _controller.GetCustodianBanks(default);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedBanks = Assert.IsAssignableFrom<IEnumerable<CustodianBank>>(okResult.Value);
            Assert.Equal(banks.Count, returnedBanks.Count());
        }

        [Fact]
        public async Task GetCustodianBanks_ReturnsNotFound_WhenNoBanksExist()
        {
            // Arrange
            var banks = new List<CustodianBank>();
            _mockRepository.Setup(repo => repo.All(default)).ReturnsAsync(banks);

            // Act
            var result = await _controller.GetCustodianBanks(default);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetCustodianBank_ReturnsOkResult_WhenBankExists()
        {
            // Arrange
            var bankId = 1;
            var bank = new CustodianBank { Id = bankId, Name = "Bank 1" };
            _mockRepository.Setup(repo => repo.Find(It.IsAny<Expression<Func<CustodianBank, bool>>>(), default))
                .ReturnsAsync([bank]);

            // Act
            var result = await _controller.GetCustodianBank(bankId, default);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedBank = Assert.IsType<List<CustodianBank>>(okResult.Value);
            Assert.Equal(bankId, returnedBank.First().Id);
        }

        [Fact]
        public async Task GetCustodianBank_ReturnsNotFound_WhenBankDoesNotExist()
        {
            // Arrange
            var bankId = 1;

            _mockRepository.Setup(repo => 
                repo.Find(It.IsAny<Expression<Func<CustodianBank, bool>>>(), default))
                .ReturnsAsync([]);

            // Act
            var result = await _controller.GetCustodianBank(bankId, default);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostCustodianBank_ReturnsCreatedAtAction_WhenBankIsAddedSuccessfully()
        {
            // Arrange
            var bank = new CustodianBank { Id = 1, Name = "Bank 1" };

            // Act
            var result = await _controller.PostCustodianBank(bank, default);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetCustodianBank", createdAtActionResult.ActionName);
            Assert.Equal(bank.Id, createdAtActionResult.RouteValues["Id"]);
            Assert.Equal(bank, createdAtActionResult.Value);
        }

        [Fact]
        public async Task PostCustodianBank_ReturnsConflict_WhenBankAlreadyExists()
        {
            // Arrange
            var bank = new CustodianBank { Id = 1, Name = "Bank 1" };
            _mockRepository.Setup(repo => repo.Add(bank, default))
                .Throws(new DbUpdateException());
            _mockRepository.Setup(repo => 
                repo.Find(It.IsAny<Expression<Func<CustodianBank, bool>>>(), default))
                .ReturnsAsync([bank]);

            // Act
            var result = await _controller.PostCustodianBank(bank, default);

            // Assert
            Assert.IsType<ConflictResult>(result.Result);
        }

        [Fact]
        public async Task PutCustodianBank_ReturnsNoContent_WhenBankIsUpdatedSuccessfully()
        {
            // Arrange
            var bankId = 1;
            var bank = new CustodianBank { Id = bankId, Name = "Bank 1" };
            _mockRepository.Setup(repo => 
                repo.Find(It.IsAny<Expression<Func<CustodianBank, bool>>>(), default))
                .ReturnsAsync([bank]);

            // Act
            var result = await _controller.PutCustodianBank(bankId, bank, default);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutCustodianBank_ReturnsBadRequest_WhenBankIdDoesNotMatch()
        {
            // Arrange
            var bankId = 1;
            var bank = new CustodianBank { Id = 2, Name = "Bank 2" };

            // Act
            var result = await _controller.PutCustodianBank(bankId, bank, default);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task PutCustodianBank_ReturnsBadRequest_WhenBankDoesNotExist()
        {
            // Arrange
            var bankId = 1;
            _mockRepository.Setup(repo => repo.Find(It.IsAny<Expression<Func<CustodianBank, bool>>>(), default))
                .ReturnsAsync([]);

            // Act
            var result = await _controller.PutCustodianBank(bankId, new CustodianBank { Name = "Not Existing" }, default);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteCustodianBank_ReturnsNoContent_WhenBankIsDeletedSuccessfully()
        {
            // Arrange
            var bankId = 1;
            var bank = new CustodianBank { Id = bankId, Name = "Bank 1" };
            _mockRepository.Setup(repo => repo.Find(It.IsAny<Expression<Func<CustodianBank, bool>>>(), default))
                .ReturnsAsync([bank]);

            // Act
            var result = await _controller.DeleteCustodianBank(bankId, default);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteCustodianBank_ReturnsNotFound_WhenBankDoesNotExist()
        {
            // Arrange
            var bankId = 1;
            _mockRepository.Setup(repo => repo.Find(It.IsAny<Expression<Func<CustodianBank, bool>>>(), default))
                .ReturnsAsync(new List<CustodianBank>());

            // Act
            var result = await _controller.DeleteCustodianBank(bankId, default);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
