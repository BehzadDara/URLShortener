using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using URLShortener.Application.Features.Commands.CreateURL;
using URLShortener.Application.Features.Queries.GetAllURLs;
using URLShortener.Application.Features.Queries.GetURLByShortened;
using URLShortener.Application.ViewModels;

namespace URLShortener.API.Controllers;


[SwaggerTag("URLShortener service")]
[Route("urls")]
public class URLController(IMediator mediator) : ControllerBase
{
    [SwaggerOperation("Make a URL shorter")]
    [SwaggerResponse(StatusCodes.Status200OK, "Created", typeof(URLViewModel))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid URL", typeof(void))]
    [SwaggerResponse(StatusCodes.Status409Conflict, "URL already exists", typeof(void))]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateURLCommand request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [SwaggerOperation("Get all URLs")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retrieved", typeof(IReadOnlyList<URLViewModel>))]
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllURLsQuery(), cancellationToken);
        return Ok(result);
    }

    [SwaggerOperation("Redirect short URL to original URL")]
    [SwaggerResponse(StatusCodes.Status301MovedPermanently, "Redirect", typeof(void))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "URL not found", typeof(void))]
    [HttpGet("{input}")]
    public async Task<IActionResult> Get(string input, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetURLByShortenedQuery(input), cancellationToken);
        return RedirectPermanent(result);
    }

    [HttpOptions]
    public IActionResult OptionsForUrls()
    {
        Response.Headers.Append("Allow", "GET, POST, OPTIONS");
        return Ok();
    }

    [HttpOptions("{input}")]
    public IActionResult OptionsForShortenedUrl()
    {
        Response.Headers.Append("Allow", "GET, OPTIONS");
        return Ok();
    }
}
