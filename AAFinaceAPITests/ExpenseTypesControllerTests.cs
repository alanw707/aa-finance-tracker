using AAExpenseTracker.Domain.Data;
using AAExpenseTracker.Domain.Entities;
using AAFinanceTracker.API.Controllers;
using AAFinanceTracker.Infrastructure.Repositories;
using Moq;

namespace AAFinaceAPITests
{
    public class ExpenseTypesControllerTests
    {
        private Mock<FinanceTrackerDbContext> _mockContext;
        private ExpenseTypesController _controller;

        [Fact]
        //TODO: We should do repository pattern and rewrite this test
        public async Task GetExpenseTypes_ReturnsOkObjectResultWithExpenseTypes()
        {
            // Arrange
            var expenseTypes = new List<ExpenseType>
            {
                new ("Credit", "Credit Card"),
                new ("Debit","Debit Card")
            };

            // Mock ExpenseTypes DbSet directly
            var repo = new Mock<IRepository<ExpenseType>>();

            // Inject mocked context into controller
            _controller = new ExpenseTypesController(repo.Object);

            // Act
            var result = await _controller.PostExpenseType(expenseTypes[0], new CancellationToken());

            // Assert
            Assert.NotNull(result);
            repo.Verify(r => r.Add(It.IsAny<ExpenseType>(),It.IsAny<CancellationToken>()),Times.Exactly(1));
        }
    }
}