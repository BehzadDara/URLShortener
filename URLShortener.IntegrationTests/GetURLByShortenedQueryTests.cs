using FluentAssertions;
using Microsoft.Extensions.Options;
using URLShortener.Application.Configs;
using URLShortener.Application.Exceptions;
using URLShortener.Application.Features.Commands.CreateURL;
using URLShortener.Application.Features.Queries.GetURLByShortened;
using URLShortener.Infrastructure.Implementations;
using URLShortener.IntegrationTests.Fixtures;
using Xunit;

namespace URLShortener.IntegrationTests;

public class GetURLByShortenedQueryTests(URLShortenerDBContextFixture fixture) : IClassFixture<URLShortenerDBContextFixture>
{
    [Fact]
    public async Task Handle_ShouldReturnURL_WhenShortenedExists()
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
        var createResponse = await createURLCommandHandler.Handle(command, CancellationToken.None);
        var shortened = createResponse.Value!.Shortened.Split('/').Last();

        var query = new GetURLByShortenedQuery(shortened);
        var getURLByShortenedQueryHandler = new GetURLByShortenedQueryHandler(repository);


        // Act
        var response = await getURLByShortenedQueryHandler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Should().Be(original);
    }

    [Fact]
    public async Task Handle_ShouldIncreaseClickCount_WhenShortenedExists()
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
        var createResponse = await createURLCommandHandler.Handle(command, CancellationToken.None);
        var shortened = createResponse.Value!.Shortened.Split('/').Last();

        var query = new GetURLByShortenedQuery(shortened);
        var getURLByShortenedQueryHandler = new GetURLByShortenedQueryHandler(repository);
        _ = await getURLByShortenedQueryHandler.Handle(query, CancellationToken.None);


        // Act
        var response = await repository.GetByShortenedAsync(shortened, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response!.ClickCount.Should().Be(1);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenShortenedDoesNotExist()
    {
        // Arrange
        var repository = new URLRepository(fixture.BuildDbContext(Guid.NewGuid().ToString()));

        var query = new GetURLByShortenedQuery("TEST");
        var getURLByShortenedQueryHandler = new GetURLByShortenedQueryHandler(repository);


        // Act
        Func<Task> act = async () => await getURLByShortenedQueryHandler.Handle(query, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}
