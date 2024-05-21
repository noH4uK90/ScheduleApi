namespace Schedule.Core.Models;

public partial class Lesson
{
    public int LessonId { get; set; }

    public int DisciplineId { get; set; }

    public int? ClassroomId { get; set; }

    public int? TeacherId { get; set; }

    public int TypeId { get; set; }

    public int TimetableId { get; set; }

    public int Number { get; set; }

    public string TimeStart { get; set; } = null!;

    public string TimeEnd { get; set; } = null!;

    public int? SubLessonId { get; set; }

    public virtual Classroom? Classroom { get; set; }

    public virtual Discipline Discipline { get; set; } = null!;

    public virtual SubLesson? SubLesson { get; set; }

    public virtual TeacherFullName? Teacher { get; set; }

    public virtual Timetable Timetable { get; set; } = null!;

    public virtual LessonType Type { get; set; } = null!;
}
