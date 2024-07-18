using AAFinanceTracker.Domain.Data;
using Entities = AAFinanceTracker.Domain.Entities;
using AAFinanceTracker.Infrastructure.Repositories;
using AAFinanceTracker.Infrastructure.Repositories.CustodianBank;
using AAFinanceTracker.Infrastructure.Repositories.Expense;
using AAFinanceTracker.Infrastructure.Repositories.Investment;
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

builder.Services.AddTransient<IRepository<Entities.ExpenseCategory>, ExpenseCategoryRepository>();
builder.Services.AddTransient<IRepository<Entities.ExpenseType>, ExpenseTypeRepository>();
builder.Services.AddTransient<IRepository<Entities.Expense>, ExpenseRepository>();

builder.Services.AddTransient<IExpenseRepository, ExpenseRepository>();
builder.Services.AddTransient<IRepository<Entities.Investment>, InvestmentRepository>();
builder.Services.AddTransient<IRepository<Entities.InvestmentType>, InvestmentTypeRepository>();
builder.Services.AddTransient<IInvestmentRepository, InvestmentRepository>();
builder.Services.AddTransient<IRepository<Entities.CustodianBank>, CustodianBankRepository>();

// Add repositories for other entities here
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
