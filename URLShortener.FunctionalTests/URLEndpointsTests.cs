using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using URLShortener.Application.Features.Commands.CreateURL;
using Xunit;

namespace URLShortener.FunctionalTests;

public class URLEndpointsTests
{
    private readonly HttpClient _httpClient;

    public URLEndpointsTests()
    {
        _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5249") };
    }

    [Fact]
    public async Task CreateURL_ShouldReturnNewURL()
    {
        // Arrange
        var original = "http://google.com/3";
        var command = new CreateURLCommand(original);
        var content = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");

        // Act
        var response = await _httpClient.PostAsync("urls", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

    }

    [Fact]
    public async Task CreateURL_ShouldReturnConflict_WhenOriginalExists()
    {
        // Arrange
        var original = "http://google.com/4";
        var command = new CreateURLCommand(original);
        var content = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");

        // Act
        _ = await _httpClient.PostAsync("urls", content);
        var response = await _httpClient.PostAsync("urls", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);

    }

    [Fact]
    public async Task GetURLByShortened_ShouldReturnNotFound_WhenShortenedDoesNotExist()
    {
        // Arrange
        var shortened = "TEST";

        // Act
        var response = await _httpClient.GetAsync($"urls/{shortened}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

    }
}
