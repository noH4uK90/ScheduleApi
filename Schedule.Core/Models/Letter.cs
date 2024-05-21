using MimeKit.Text;

namespace Schedule.Core.Models;

public class Letter
{
    public required string From { get; set; }
    public required string To { get; set; }
    public required string Subject { get; set; }
    public required string Message { get; set; }
    public required TextFormat Format { get; set; } = TextFormat.Text;
}