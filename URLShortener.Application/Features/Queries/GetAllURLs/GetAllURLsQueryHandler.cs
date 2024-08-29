using MediatR;
using URLShortener.Application.ViewModels;
using URLShortener.Domain.Repositories;

namespace URLShortener.Application.Features.Queries.GetAllURLs;

public class GetAllURLsQueryHandler(IURLRepository repository) : IRequestHandler<GetAllURLsQuery, ResultViewModel<IReadOnlyList<URLViewModel>>>
{
    public async Task<ResultViewModel<IReadOnlyList<URLViewModel>>> Handle(GetAllURLsQuery request, CancellationToken cancellationToken)
    {
        var urls = await repository.GetAllAsync(cancellationToken);
        var viewModels = urls.ToViewModel();

        return ResultViewModel<IReadOnlyList<URLViewModel>>.OK(viewModels, "Retrevied!");
    }
}
