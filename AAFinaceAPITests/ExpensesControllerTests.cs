using System.Linq.Expressions;
using AAExpenseTracker.Domain.Entities;
using AAFinanceTracker.API.Controllers;
using AAFinanceTracker.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AAFinanceTracker.API.Tests
{
    public class ExpensesControllerTests
    {
        [Fact]
        public async Task GetExpenses_ReturnsAllExpenses()
        {
            // Arrange
            var mockExpenseRepository = new Mock<IRepository<Expense>>();
            mockExpenseRepository.Setup(repo => repo.All(It.IsAny<CancellationToken>()))
                .ReturnsAsync([new(), new(), new()]);

            var controller = new ExpensesController(mockExpenseRepository.Object);

            // Act
            var result = await controller.GetExpenses(CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<Expense>>>(result);
            var expenses = Assert.IsAssignableFrom<IEnumerable<Expense>>(okResult.Value);

            Assert.Equal(3, expenses.Count());
        }
        [Fact]
        public async Task GetExpense_ReturnsExpense_WhenFound()
        {
            // Arrange
            var expenseId = 12;
            var expense = new Expense { ExpenseId = expenseId };

            var mockExpenseRepository = new Mock<IRepository<Expense>>();
            mockExpenseRepository.Setup(repo => 
                    repo.Find(It.IsAny<Expression<Func<Expense, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync([expense]);

            var controller = new ExpensesController(mockExpenseRepository.Object);

            // Act
            var result = await controller.GetExpense(expenseId, CancellationToken.None);

            // Assert
            Assert.IsType<ActionResult<Expense>>(result);
            var returnedExpense = Assert.IsAssignableFrom<Expense>(result.Value);
            Assert.Equal(expense, returnedExpense);
        }

        [Fact]
        public async Task GetExpense_ReturnsNotFound_WhenExpenseNotFound()
        {
            // Arrange
            var expenseId = 123;
            var mockExpenseRepository = new Mock<IRepository<Expense>>();
            mockExpenseRepository.Setup(repo => repo.Find(It.IsAny<Expression<Func<Expense, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Expense>());

            var controller = new ExpensesController(mockExpenseRepository.Object);

            // Act
            var result = await controller.GetExpense(expenseId, CancellationToken.None);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
