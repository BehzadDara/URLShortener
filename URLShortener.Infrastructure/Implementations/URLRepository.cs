using Microsoft.EntityFrameworkCore;
using URLShortener.Domain.Models;
using URLShortener.Domain.Repositories;

namespace URLShortener.Infrastructure.Implementations;

public class URLRepository(URLShortenerDBContext dbContext) : IURLRepository
{
    public async Task AddAsync(URL url, CancellationToken cancellationToken)
    {
        await dbContext.AddAsync(url, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(URL url, CancellationToken cancellationToken)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<URL?> GetByOriginalAsync(string original, CancellationToken cancellationToken)
    {
        var url = await dbContext.Set<URL>().FirstOrDefaultAsync(x => x.Original == original, cancellationToken);
        return url;
    }

    public async Task<URL?> GetByShortenedAsync(string shortened, CancellationToken cancellationToken)
    {
        var url = await dbContext.Set<URL>().FirstOrDefaultAsync(x => x.Shortened == shortened, cancellationToken);
        return url;
    }

    public async Task<IReadOnlyList<URL>> GetAllAsync(CancellationToken cancellationToken)
    {
        var urls = await dbContext.Set<URL>().ToListAsync(cancellationToken);
        return [.. urls.OrderByDescending(x => x.CreatedAt)];
    }
}
