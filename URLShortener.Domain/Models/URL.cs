using URLShortener.Domain.Helpers;

namespace URLShortener.Domain.Models;

public class URL
{
    public int Id { get; }
    public required string Original { get; init; }
    public required string Shortened { get; init; }
    public DateTime CreatedAt { get; } = DateTime.Now;
    public int ClickCount { get; private set; } = 0;

    public static URL Create(string original, List<string> shorteneds)
    {
        return new URL
        {
            Original = original,
            Shortened = MethodHelper.GenerateShortened(shorteneds)
        };
    }

    public void Visit()
    {
        ClickCount++;
    }
}
