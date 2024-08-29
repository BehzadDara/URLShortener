﻿using MediatR;
using Microsoft.Extensions.Options;
using URLShortener.Application.Configs;
using URLShortener.Application.Exceptions;
using URLShortener.Application.ViewModels;
using URLShortener.Domain.Models;
using URLShortener.Domain.Repositories;
using URLShortener.Resources;

namespace URLShortener.Application.Features.Commands.CreateURL;

public class CreateURLCommandHandler(IURLRepository repository, IOptions<Settings> optionSettings) : IRequestHandler<CreateURLCommand, ResultViewModel<URLViewModel>>
{
    private readonly Settings settings = optionSettings.Value;

    public async Task<ResultViewModel<URLViewModel>> Handle(CreateURLCommand request, CancellationToken cancellationToken)
    {
        var tmp = await repository.GetByOriginalAsync(request.Original, cancellationToken);
        if (tmp is not null)
        {
            throw new ConflictException(Messages.RepetitiveOriginalURL);
        }

        var url = URL.Create(request.Original);
        await repository.AddAsync(url, cancellationToken);

        return ResultViewModel<URLViewModel>.OK(url.ToViewModel(settings.BaseURL), Messages.Success);
    }
}
