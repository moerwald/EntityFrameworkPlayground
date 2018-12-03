using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace EntityTest
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        public IEnumerable<DbEntityEntry> GetModifiedEntries() =>
            this.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasOptional(u => u.Blog)
                        .WithRequired(b => b.User);
        }
    }
}
