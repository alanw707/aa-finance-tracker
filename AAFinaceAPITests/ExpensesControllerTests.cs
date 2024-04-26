using System.Linq.Expressions;
using AAExpenseTracker.Domain.Entities;
using AAFinanceTracker.API.Controllers;
using AAFinanceTracker.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;

namespace AAFinanceTracker.API.Tests
{
    public class ExpensesControllerTests
    {
        [Fact]
        public async Task GetExpenses_ReturnsAllExpenses()
        {
            // Arrange
            var expectedExpenses = new List<Expense>()
            {
                new() { ExpenseCategoryName = "Transportation", ExpenseTypeName = "Credit Card" },
                new()  { ExpenseCategoryName ="Grocery", ExpenseTypeName = "Apple Pay" }
            };

            var mockExpenseRepository = new Mock<IRepository<Expense>>();
            mockExpenseRepository.Setup(repo => repo.All(It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedExpenses);

            var controller = new ExpensesController(mockExpenseRepository.Object);

            // Act
            var result = await controller.GetExpenses(CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<Expense>>>(result);
            var expenses = Assert.IsAssignableFrom<IEnumerable<Expense>>(okResult.Value);

            Assert.Equal(2, expenses.Count());
        }
        [Fact]
        public async Task GetExpense_ReturnsExpense_WhenFound()
        {
            // Arrange
            var expenseId = 12;
            var expense = new Expense { ExpenseId = expenseId, ExpenseCategoryName = "Transportation", ExpenseTypeName = "Credit Card" };

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

        [Fact]
        public async Task PutExpense_InvalidId_ReturnsBadRequest()
        {
            // Arrange            
            var mockRepository = new Mock<IRepository<Expense>>();

            // Act
            var controller = new ExpensesController(mockRepository.Object);
            var result = await controller.PutExpense(123, new Expense() { ExpenseCategoryName = "", ExpenseTypeName = "" }, CancellationToken.None);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async Task PutExpense_ValidId_ReturnsNoContent_WhenExpenseExists()
        {
            // Arrange
            var expenseId = 1;
            var expense = new Expense { ExpenseId = expenseId, ExpenseCategoryName = "", ExpenseTypeName = "" };
            var mockExpenseRepository = new Mock<IRepository<Expense>>();

            mockExpenseRepository.Setup(repo => repo.Update(It.IsAny<Expense>()))
            .Returns(expense);

            var controller = new ExpensesController(mockExpenseRepository.Object);

            // Act
            var result = await controller.PutExpense(expenseId, expense, CancellationToken.None);

            // Assert        
            mockExpenseRepository.Verify(x => x.Update(It.Is<Expense>(e => e.ExpenseId == expenseId)), Times.Once);
            mockExpenseRepository.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PostExpense_ReturnsCreatedAtAction()
        {
            // Arrange
            var expectedModel = new Models.ExpenseModel()
            {
                CategoryName = "Travel",
                TypeName = "Credit Card",
                Comments = "Test comments",
                Amount = 10
            };

            var expense = new Expense()
            {
                ExpenseCategory = new ExpenseCategory() { Name = expectedModel.CategoryName },
                ExpenseType = new ExpenseType() { Name = expectedModel.TypeName },
                ExpenseCategoryName = expectedModel.CategoryName,
                ExpenseTypeName = expectedModel.TypeName,
                Comments = expectedModel.Comments,
                Amount = expectedModel.Amount
            };

            var mockExpenseRepository = new Mock<IRepository<Expense>>();

            mockExpenseRepository
             .Setup(repo => repo.Add(It.IsAny<Expense>(), CancellationToken.None))
             .ReturnsAsync(It.IsAny<EntityEntry<Expense>>());


            var controller = new ExpensesController(mockExpenseRepository.Object);

            // Act
            var result = await controller.PostExpense(expectedModel, new CancellationToken());

            // Assert
            //mockExpenseRepository.Verify(r => r.Add(expense, It.IsAny<CancellationToken>()), Times.Exactly(1));
            mockExpenseRepository.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);

            Assert.Equal("GetExpense", createdResult.ActionName);
            Assert.Equal(expense.ExpenseId, createdResult.RouteValues!["expenseId"]);
            Assert.Equal(expense.ExpenseCategoryName, ((Expense)createdResult.Value!).ExpenseCategoryName);
            Assert.Equal(expense.ExpenseTypeName, ((Expense)createdResult.Value).ExpenseTypeName);
            Assert.Equal(expense.ExpenseId, ((Expense)createdResult.Value).ExpenseId);
        }
    }
}
