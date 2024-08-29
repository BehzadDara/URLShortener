using URLShortener.Domain.Models;

namespace URLShortener.Domain.Repositories;

public interface IURLRepository
{
    public Task AddAsync(URL url, CancellationToken cancellationToken);
    public Task UpdateAsync(URL url, CancellationToken cancellationToken);
    public Task<URL?> GetByOriginalAsync(string original, CancellationToken cancellationToken);
    public Task<URL?> GetByShortenedAsync(string shortened, CancellationToken cancellationToken);
    public Task<IReadOnlyList<URL>> GetAllAsync(CancellationToken cancellationToken);
}
