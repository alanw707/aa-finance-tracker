using System.CodeDom;
using aa_finance_tracker.Domains;
using Microsoft.EntityFrameworkCore;

namespace aa_finance_tracker.Data
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
                .HasKey(e => e.Id);

            modelBuilder.Entity<ExpenseType>()
                .HasKey(e => e.Name);

            modelBuilder.Entity<ExpenseCategory>()
               .HasKey(e => e.Name);

            modelBuilder.Entity<Expense>()
                .Property(e => e.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            modelBuilder.Entity<ExpenseCategory>()
                .Property(e => e.Budget)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        }

    }
}
