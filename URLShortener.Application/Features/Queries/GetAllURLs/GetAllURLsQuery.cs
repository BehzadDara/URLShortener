using MediatR;
using URLShortener.Application.ViewModels;

namespace URLShortener.Application.Features.Queries.GetAllURLs;

public record GetAllURLsQuery : IRequest<ResultViewModel<IReadOnlyList<URLViewModel>>>;
