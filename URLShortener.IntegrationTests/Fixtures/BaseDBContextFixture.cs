using Microsoft.EntityFrameworkCore;

namespace URLShortener.IntegrationTests.Fixtures;

public abstract class BaseDBContextFixture<TDbContext>
    : IDisposable where TDbContext : DbContext
{
    public TDbContext BuildDbContext(string dbName)
    {
        try
        {
            var options = new DbContextOptionsBuilder<TDbContext>()
                            .UseInMemoryDatabase(dbName)
                            .EnableSensitiveDataLogging()
                            .Options;

            var db = BuildDbContext(options);
            db.Database.EnsureCreated();

            return BuildDbContext(options);
        }
        catch (Exception ex)
        {
            throw new Exception($"unable to connect to db.", ex);
        }
    }

    protected abstract TDbContext BuildDbContext(DbContextOptions<TDbContext> options);

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}