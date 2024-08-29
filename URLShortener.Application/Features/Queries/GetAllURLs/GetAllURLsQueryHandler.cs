using MediatR;
using Microsoft.Extensions.Options;
using URLShortener.Application.Configs;
using URLShortener.Application.ViewModels;
using URLShortener.Domain.Repositories;
using URLShortener.Resources;

namespace URLShortener.Application.Features.Queries.GetAllURLs;

public class GetAllURLsQueryHandler(IURLRepository repository, IOptions<Settings> optionSettings) : IRequestHandler<GetAllURLsQuery, ResultViewModel<IReadOnlyList<URLViewModel>>>
{
    private readonly Settings settings = optionSettings.Value;

    public async Task<ResultViewModel<IReadOnlyList<URLViewModel>>> Handle(GetAllURLsQuery request, CancellationToken cancellationToken)
    {
        var urls = await repository.GetAllAsync(cancellationToken);
        var viewModels = urls.ToViewModel(settings.BaseURL);

        return ResultViewModel<IReadOnlyList<URLViewModel>>.OK(viewModels, Messages.Success);
    }
}
