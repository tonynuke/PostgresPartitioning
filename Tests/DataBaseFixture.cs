using Microsoft.EntityFrameworkCore;
using Persistence;
using System;

namespace Tests
{
    public class DataBaseFixture : IDisposable
    {
        private readonly ApplicationContext _context;

        public ApplicationContext Context => _context;

        public DataBaseFixture()
        {
            var x = EmbeddedResourceLoader.ReadResourceFile(
                "Persistence.Sql.CreatePurchasesTable.txt");

            var factory = new ApplicationDbContextFactory();
            _context = factory.CreateDbContext(Array.Empty<string>());

            _context.Database.ExecuteSqlRaw(@"DROP SCHEMA public CASCADE; CREATE SCHEMA public;");
            _context.Database.Migrate();
        }

        public void Dispose()
        {
            //_context.Database.EnsureDeleted();
        }
    }
}