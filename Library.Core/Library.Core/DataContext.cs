using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Library.Core.Entities;

namespace Library.Core
{
    public class DataContext : IdentityDbContext<Users>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> User => Set<User>();
        public DbSet<Roles> AppRoles => Set<Roles>();
        public DbSet<Tests> Tests => Set<Tests>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
