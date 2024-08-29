using MediatR;
using Microsoft.AspNetCore.Mvc;
using URLShortener.Application.Features.Commands.CreateURL;
using URLShortener.Application.Features.Queries.GetAllURLs;
using URLShortener.Application.Features.Queries.GetURLByShortened;

namespace URLShortener.API.Controllers;

[Route("[controller]/[action]")]
public class URLController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateURLCommand request, CancellationToken cancellationToken)
    {
        await mediator.Send(request, cancellationToken);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var urls = await mediator.Send(new GetAllURLsQuery(), cancellationToken);
        return Ok(urls);
    }

    [HttpGet]
    public async Task<IActionResult> Get(GetURLByShortenedQuery request, CancellationToken cancellationToken)
    {
        var url = await mediator.Send(request, cancellationToken);
        return Ok(url);
    }
}
