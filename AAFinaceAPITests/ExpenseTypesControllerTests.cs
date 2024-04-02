using aa_finance_tracker.Controllers;
using aa_finance_tracker.Data;
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
            _mockContext = new Mock<FinanceTrackerDbContext>();

            _mockContext.Setup(x => x.ExpenseTypes)
                .ReturnsDbSet(expenseTypes);

            // Inject mocked context into controller
            _controller = new ExpenseTypesController(_mockContext.Object);

            // Act
            var result = (await _controller.GetExpenseTypes()).Value;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

    }
}