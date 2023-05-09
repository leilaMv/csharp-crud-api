namespace csharp_crud_api.DataContext
{
    using Models;
    using Microsoft.EntityFrameworkCore;

    public class dbContext:DbContext
    {
      
        public dbContext(DbContextOptions<dbContext> options) : base(options)
        {
        }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
