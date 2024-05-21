namespace Schedule.Core.Models;

public class ParsedScheduleItem
{
    public int Number { get; set; }
    public int DayId { get; set; }
    public string Discipline { get; set; } = null!;
    public string? Teacher { get; set; }
    public string? Classroom { get; set; }
    public string Type { get; set; } = null!;
    public ParsedScheduleSubItem? SubItem { get; set; }
}

public class ParsedScheduleSubItem
{
    public int Number { get; set; }
    public int DayId { get; set; }
    public string Discipline { get; set; } = null!;
    public string? Teacher { get; set; }
    public string? Classroom { get; set; }
    public string Type { get; set; } = null!;
}