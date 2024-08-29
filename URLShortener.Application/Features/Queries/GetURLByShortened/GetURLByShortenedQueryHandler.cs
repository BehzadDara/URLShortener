using MediatR;
using URLShortener.Application.Exceptions;
using URLShortener.Domain.Repositories;

namespace URLShortener.Application.Features.Queries.GetURLByShortened;

public class GetURLByShortenedQueryHandler(IURLRepository repository) : IRequestHandler<GetURLByShortenedQuery, string>
{
    public async Task<string> Handle(GetURLByShortenedQuery request, CancellationToken cancellationToken)
    {
        var url = await repository.GetByShortenedAsync(request.Shortened, cancellationToken)
            ?? throw new NotFoundException("Not found!");

        url.Visit();
        await repository.UpdateAsync(url, cancellationToken);

        return url.Original;
    }
}
