using FluentAssertions;
using URLShortener.Domain.Models;
using Xunit;

namespace URLShortener.UnitTests;

public class URLTests
{
    [Fact]
    public void Create_ShouldReturnURLWithOriginal()
    {
        // Arrange
        var original = "http://google.com/";

        // Act
        var url = URL.Create(original);

        // Assert
        url.Original.Should().Be(original);
    }

    [Fact]
    public void Create_ShouldReturnURLWith5LengthShortened()
    {
        // Arrange
        var original = "http://google.com/";

        // Act
        var url = URL.Create(original);

        // Assert
        url.Shortened.Length.Should().Be(5);
    }

    [Fact]
    public void Visit_ShouldIncreaseClickCount()
    {
        // Arrange
        var original = "http://google.com/";

        // Act
        var url = URL.Create(original);
        url.Visit();

        // Assert
        url.ClickCount.Should().Be(1);
    }
}
