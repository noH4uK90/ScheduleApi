namespace Schedule.Core.Models;

public partial class Classroom
{
    public int ClassroomId { get; set; }

    public string Cabinet { get; set; } = null!;

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual ICollection<SubLesson> SubLessons { get; set; } = new List<SubLesson>();
}
