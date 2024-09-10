using Microsoft.EntityFrameworkCore;
using URLShortener.Infrastructure;

namespace URLShortener.IntegrationTests.Fixtures;

public class URLShortenerDBContextFixture : BaseDBContextFixture<URLShortenerDBContext>
{
    protected override URLShortenerDBContext BuildDbContext(DbContextOptions<URLShortenerDBContext> options)
    {
        return new URLShortenerDBContext(options);
    }
}