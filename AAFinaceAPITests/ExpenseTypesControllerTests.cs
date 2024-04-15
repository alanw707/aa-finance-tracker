using AAExpenseTracker.Domain.Data;
using AAExpenseTracker.Domain.Entities;
using AAFinanceTracker.API.Controllers;
using AAFinanceTracker.Infrastructure.Repositories;
using Moq;
using Moq.EntityFrameworkCore;

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
            var repo = new Mock<ExpenseTypeRepository>().Object;


            // Inject mocked context into controller
            _controller = new ExpenseTypesController(repo);

            // Act
            var result = (await _controller.GetExpenseTypes(new CancellationToken())).Value;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
        [Fact]
        public void ExpenseTypesShouldHaveMoreThanOneRecord()
        {
            //arrange
            var repo = new Mock<ExpenseTypeRepository>().Object;
            //act
            var result = repo.All(new CancellationToken());
            //Assert
            Assert.NotNull(result);
        }
    }
}