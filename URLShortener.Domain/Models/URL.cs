using URLShortener.Domain.Helpers;

namespace URLShortener.Domain.Models;

public class URL
{
    public required string Original { get; init; }
    public required string Shortened { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.Now;
    public int ClickCount { get; private set; } = 0;

    public static URL Create(string original)
    {
        return new URL
        {
            Original = original,
            Shortened = MethodHelper.GenerateShortened()
        };
    }

    public void Visit()
    {
        ClickCount++;
    }
}
