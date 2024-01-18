using HackerNews.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HackerNews.Repositories
{
    public class ApplicationDbContext : DbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Story>? Story { get; set; }
      

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
           return base.SaveChangesAsync(acceptAllChangesOnSuccess: true, cancellationToken);
        }
    }
}
