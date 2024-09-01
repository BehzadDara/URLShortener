using Microsoft.EntityFrameworkCore;
using URLShortener.Domain.Models;

namespace URLShortener.Infrastructure;

public class URLShortenerDBContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<URL> URLs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<URL>().HasKey(x => x.Original);
        modelBuilder.Entity<URL>().HasKey(x => x.Shortened);
    }
}
