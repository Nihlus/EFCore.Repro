using Microsoft.EntityFrameworkCore;

namespace EFCore.Repro
{
    public class ReproDatabaseContext : DbContext
    {
        public DbSet<ReproEntity> Entities { get; }

        public ReproDatabaseContext(DbContextOptions<ReproDatabaseContext> options) : base(options)
        {
        }
    }
}
