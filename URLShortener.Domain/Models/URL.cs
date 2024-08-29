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
            Shortened = original
        };
    }

    public void Visit()
    {
        ClickCount++;
    }
}
