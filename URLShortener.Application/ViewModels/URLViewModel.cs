namespace URLShortener.Application.ViewModels;

public record URLViewModel
    (
    int Id,
    string Original,
    string Shortened,
    DateTime CreatedAt,
    int ClickCount
    );


/*
 public class URLViewModel
{
    public int Id { get; set; }
    public required string Original { get; set; }
    public required string Shortened { get; set; }
    public DateTime CreatedAt { get; set; }
    public int ClickCount { get; set; }
}
 */