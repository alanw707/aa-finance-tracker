using AAExpenseTracker.Domain.Data;
using AAExpenseTracker.Domain.Entities;
using AAFinanceTracker.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
// Add services to the container.
builder.Services.AddDbContext<FinanceTrackerDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("LocalDockerSQL")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IRepository<ExpenseCategory>, ExpenseCategoryRepository>();
builder.Services.AddTransient<IRepository<ExpenseType>, ExpenseTypeRepository>();
builder.Services.AddTransient<IRepository<Expense>, ExpenseRepository>();
builder.Services.AddTransient<IExpenseRepository, ExpenseRepository>();
builder.Services.AddTransient<IRepository<Investment>, InvestmentRepository>();
builder.Services.AddTransient<IRepository<InvestmentType>, InvestmentTypeRepository>();
// ... other services and configurations ...
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
