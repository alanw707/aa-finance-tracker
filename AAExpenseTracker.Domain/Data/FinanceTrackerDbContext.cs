using AAExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace AAExpenseTracker.Domain.Data
{
    public class FinanceTrackerDbContext(IConfiguration _configuration, DbContextOptions<FinanceTrackerDbContext> options)
            : DbContext(options)
    {
        public virtual DbSet<ExpenseType>? ExpenseTypes { get; set; }
        public DbSet<ExpenseCategory>? ExpensesCategories { get; set; }
        public DbSet<Expense>? Expenses { get; set; }
        public DbSet<Investment>? Investments { get; set; }
        public DbSet<CustodianBank>? CustodianBanks { get; set; }
        public DbSet<InvestmentType>? InvestmentTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("LocalDockerSQL"));
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

            modelBuilder.Entity<Investment>()
                .HasKey(k => k.Id);
            modelBuilder.Entity<Investment>()
                .Property(k => k.InitialInvestment)
                .HasColumnType("decimal(18,2)").IsRequired();

            modelBuilder.Entity<InvestmentType>()
               .HasKey(k => k.Name);

            modelBuilder.Entity<CustodianBank>()
                .HasKey(k => k.Id);            
        }

    }
}
