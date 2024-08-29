using MediatR;

namespace URLShortener.Application.Features.Queries.GetURLByShortened;

public record GetURLByShortenedQuery(string Shortened) : IRequest<string>;
