using FreeBilling.Web.Data.Entitis;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FreeBilling.Web.Data
{
    public class BillingContext:IdentityDbContext<TimeBillUsers>
    {
        public BillingContext(string connectionString) :
      this(new DbContextOptionsBuilder().UseSqlServer(connectionString).Options)
        {

        }
        public BillingContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Employee> Employees  => Set<Employee>();
        public DbSet<TimeBill> TimeBills => Set<TimeBill>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly
                (typeof(BillingContext).Assembly);

        }
    }
     
}
