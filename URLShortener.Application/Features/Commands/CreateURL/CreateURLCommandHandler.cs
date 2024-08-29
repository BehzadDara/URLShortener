using MediatR;
using URLShortener.Application.Exceptions;
using URLShortener.Application.ViewModels;
using URLShortener.Domain.Models;
using URLShortener.Domain.Repositories;

namespace URLShortener.Application.Features.Commands.CreateURL;

public class CreateURLCommandHandler(IURLRepository repository) : IRequestHandler<CreateURLCommand, ResultViewModel>
{
    public async Task<ResultViewModel> Handle(CreateURLCommand request, CancellationToken cancellationToken)
    {
        var tmp = await repository.GetByOriginalAsync(request.Original, cancellationToken);
        if (tmp is not null)
        {
            throw new ConflictException("URL already exists!");
        }

        var url = URL.Create(request.Original);
        await repository.AddAsync(url, cancellationToken);

        return ResultViewModel.OK("Created!");
    }
}
