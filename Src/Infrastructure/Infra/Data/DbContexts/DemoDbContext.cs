using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.DbContexts
{
    public sealed class DemoDbContext: DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> dbContextOptions):base(dbContextOptions)
        {
            
        }

        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DemoDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
