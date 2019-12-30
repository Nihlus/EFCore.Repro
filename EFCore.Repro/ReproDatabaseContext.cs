using Microsoft.EntityFrameworkCore;

namespace EFCore.Repro
{
    public class ReproDatabaseContext : DbContext
    {
        public DbSet<ReproEntity> Entities { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=:memory:");
        }
    }
}
