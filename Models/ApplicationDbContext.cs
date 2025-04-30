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
        public DbSet<Customer> Customers { get; set; }  // Add this line
        public DbSet<Employee> Employees { get; set; }  // Add this line
    }
}
