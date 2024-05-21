namespace Schedule.Application.ViewModels;

public sealed class ReportViewModel
{
    public required byte[] Content { get; set; }
    public required string ContentType { get; set; }
    public required string ReportName { get; set; }
}