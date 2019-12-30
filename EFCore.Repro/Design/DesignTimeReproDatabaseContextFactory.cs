using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EFCore.Repro.Design
{
    /// <summary>
    /// Design-time factory for <see cref="ReproDatabaseContext"/> instances.
    /// </summary>
    public class DesignTimeReproDatabaseContextFactory : IDesignTimeDbContextFactory<ReproDatabaseContext>
    {
        /// <inheritdoc />
        public ReproDatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ReproDatabaseContext>();
            optionsBuilder.UseSqlite("DataSource=:memory:");

            return new ReproDatabaseContext(optionsBuilder.Options);
        }
    }
}
