using EStore.Models;
using Microsoft.EntityFrameworkCore;

namespace EStore.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> User { get; set; }
        /* public ApplicationContext()
         {
             Database.EnsureCreated();
         }*/
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
