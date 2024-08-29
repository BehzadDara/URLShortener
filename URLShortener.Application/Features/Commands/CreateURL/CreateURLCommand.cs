using MediatR;
using URLShortener.Application.ViewModels;

namespace URLShortener.Application.Features.Commands.CreateURL;

public record CreateURLCommand(string Original) : IRequest<ResultViewModel<URLViewModel>>;