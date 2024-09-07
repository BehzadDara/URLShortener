namespace URLShortener.Application.ViewModels;

public class URLViewModel
{
    public required string Original { get; set; }
    public required string Shortened { get; set; }
    public DateTime CreatedAt { get; set; }
    public int ClickCount { get; set; }
}