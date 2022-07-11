using EStore.Models;
using Microsoft.EntityFrameworkCore;

namespace EStore.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Goods> Goods { get; set; }
        public DbSet<Orders> Orders { get; set; }
       
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
