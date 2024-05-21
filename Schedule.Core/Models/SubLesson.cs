namespace Schedule.Core.Models;

public partial class SubLesson
{
    public int SubLessonId { get; set; }

    public int DisciplineId { get; set; }

    public int? ClassroomId { get; set; }

    public int? TeacherId { get; set; }

    public int TypeId { get; set; }

    public virtual Classroom? Classroom { get; set; }

    public virtual Discipline Discipline { get; set; } = null!;

    public virtual Lesson? Lesson { get; set; }

    public virtual TeacherFullName? Teacher { get; set; }

    public virtual LessonType Type { get; set; } = null!;
}
