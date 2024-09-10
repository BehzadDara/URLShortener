using FluentAssertions;
using Microsoft.Extensions.Options;
using URLShortener.Application.Configs;
using URLShortener.Application.Exceptions;
using URLShortener.Application.Features.Commands.CreateURL;
using URLShortener.Application.ViewModels;
using URLShortener.Infrastructure.Implementations;
using URLShortener.IntegrationTests.Fixtures;
using Xunit;

namespace URLShortener.IntegrationTests;

public class CreateURLCommandTests(URLShortenerDBContextFixture fixture) : IClassFixture<URLShortenerDBContextFixture>
{
    [Fact]
    public async Task Handle_ShouldCreateURL_WhenOriginalDoesNotExist()
    {
        // Arrange
        var original = "http://google.com/1";
        var command = new CreateURLCommand(original);
        var repository = new URLRepository(fixture.BuildDbContext(Guid.NewGuid().ToString()));
        var options = Options.Create(new Settings
        {
            BaseURL = "http://localhost:5249/urls"
        });
        var handler = new CreateURLCommandHandler(repository, options);

        // Act
        var response = await handler.Handle(command, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeTrue();
        response.Value.Should().NotBeNull();
        response.Value!.Original.Should().Be(original);

        var url = repository.GetByOriginalAsync(original, CancellationToken.None);
        url.Should().NotBeNull();
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenOriginalExists()
    {
        // Arrange
        var original = "http://google.com/2";
        var command = new CreateURLCommand(original);
        var repository = new URLRepository(fixture.BuildDbContext(Guid.NewGuid().ToString()));
        var options = Options.Create(new Settings
        {
            BaseURL = "http://localhost:5249/urls"
        });
        var handler = new CreateURLCommandHandler(repository, options);

        // Act
        _ = await handler.Handle(command, CancellationToken.None);

        var response = new ResultViewModel<URLViewModel>();
        Func<Task> act = async () => response = await handler.Handle(command, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.IsSuccess.Should().BeFalse();
        response.Value.Should().BeNull();
        await act.Should().ThrowAsync<ConflictException>();
    }
}
