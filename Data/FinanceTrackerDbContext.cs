using Microsoft.EntityFrameworkCore;

namespace aa_finance_tracker.Data
{
    public class FinanceTrackerDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<ExpenseTypes> ExpenseTypes { get; set; }

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
            
            modelBuilder.Entity<ExpenseTypes>()
                .HasKey(e => e.Id);
        }

    }
}
