using Microsoft.EntityFrameworkCore;
using IntPaymentAPI.Models;  // Make sure to import your models

namespace IntPaymentAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
                public DbSet<Payment> Payments { get; set; }

    public DbSet<TransactionDetails> TransactionDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }  // Add this line
        public DbSet<Employee> Employees { get; set; } 
        // Override OnModelCreating if needed
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>()
          .HasOne(t => t.Customer)
    .WithMany(c => c.Transactions)
    .HasForeignKey(t => t.CustomerId); // You can change the delete behavior based on your needs
    }
        
         // Add this line
    }
}
