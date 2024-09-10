using FluentAssertions;
using Microsoft.Extensions.Options;
using URLShortener.Application.Configs;
using URLShortener.Application.Features.Commands.CreateURL;
using URLShortener.Application.Features.Queries.GetAllURLs;
using URLShortener.Application.Features.Queries.GetURLByShortened;
using URLShortener.Infrastructure.Implementations;
using URLShortener.IntegrationTests.Fixtures;
using Xunit;

namespace URLShortener.IntegrationTests;

public class GetAllURLsQueryTests(URLShortenerDBContextFixture fixture) : IClassFixture<URLShortenerDBContextFixture>
{
    [Fact]
    public async Task Handle_ShouldReturnNothing_WhenNothingAdded()
    {
        // Arrange
        var repository = new URLRepository(fixture.BuildDbContext(Guid.NewGuid().ToString()));
        var options = Options.Create(new Settings
        {
            BaseURL = "http://localhost:5249/urls"
        });

        var query = new GetAllURLsQuery();
        var getAllURLsQueryHandler = new GetAllURLsQueryHandler(repository, options);


        // Act
        var response = await getAllURLsQueryHandler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        response.Value.Should().NotBeNull();
        response.Value!.Count.Should().Be(0);
    }

    [Fact]
    public async Task Handle_ShouldReturn1URL_WhenURLAdded()
    {
        // Arrange
        var original = "http://google.com/";
        var command = new CreateURLCommand(original);
        var repository = new URLRepository(fixture.BuildDbContext(Guid.NewGuid().ToString()));
        var options = Options.Create(new Settings
        {
            BaseURL = "http://localhost:5249/urls"
        });
        var createURLCommandHandler = new CreateURLCommandHandler(repository, options);
        _ = await createURLCommandHandler.Handle(command, CancellationToken.None);

        var query = new GetAllURLsQuery();
        var getAllURLsQueryHandler = new GetAllURLsQueryHandler(repository, options);


        // Act
        var response = await getAllURLsQueryHandler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        response.Value.Should().NotBeNull();
        response.Value!.Count.Should().Be(1);
        response.Value.First().Original.Should().Be(original);
    }
}