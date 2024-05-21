namespace Schedule.Core.Models;

public partial class Timetable
{
    public int TimetableId { get; set; }

    public DateOnly Date { get; set; }

    public int GroupId { get; set; }

    public int DayId { get; set; }

    public virtual Day Day { get; set; } = null!;

    public virtual Group Group { get; set; } = null!;

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
