using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Tests
{
    public class DataBaseFixture
    {
        public DataBaseFixture()
        {
            using var context = CreateContext();

            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }

        public ApplicationContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseNpgsql(
                "Host=localhost;Database=partitions;Username=postgres;Password=postgres;Include Error Detail=true;");
            optionsBuilder.UseSnakeCaseNamingConvention();
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.EnableDetailedErrors();

            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}