﻿using AAExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AAExpenseTracker.Domain.Data
{
    public class FinanceTrackerDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public virtual DbSet<ExpenseType> ExpenseTypes { get; set; }

        public DbSet<ExpenseCategory> ExpensesCategories { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        public FinanceTrackerDbContext()
        {

        }

        public FinanceTrackerDbContext(IConfiguration configuration)
            : base(new DbContextOptionsBuilder<FinanceTrackerDbContext>()
                .UseSqlServer(configuration.GetConnectionString("LocalDockerSQL"))
                .Options)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Expense>()
                .HasKey(e => e.ExpenseId);
            modelBuilder.Entity<Expense>()
                .Property(e => e.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            modelBuilder.Entity<ExpenseType>()
                .HasKey(e => e.Name);


            modelBuilder.Entity<ExpenseCategory>()
               .HasKey(e => e.Name);

            modelBuilder.Entity<ExpenseCategory>()
                .Property(e => e.Budget)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        }

    }
}