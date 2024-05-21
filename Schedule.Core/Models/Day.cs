namespace Schedule.Core.Models;

public partial class Day
{
    public int DayId { get; set; }

    public string WeekDay { get; set; } = null!;

    public bool IsStudy { get; set; }

    public virtual ICollection<Timetable> Timetables { get; set; } = new List<Timetable>();
}
