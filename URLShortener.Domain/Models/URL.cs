using URLShortener.Domain.Helpers;

namespace URLShortener.Domain.Models;

public class URL
{
    public int Id { get; }
    public required string Original { get; init; }
    public required string Shortened { get; init; }
    public DateTime CreatedAt { get; } = DateTime.Now;
    public int ClickCount { get; private set; } = 0;

    public static URL Create(string original)
    {
        return new URL
        {
            Original = original,
            Shortened = Hash.Sha256(original)
        };
    }

    public void Visit()
    {
        ClickCount++;
    }
}
