namespace Schedule.Core.Models;

public partial class TeacherFullName
{
    public int FullNameId { get; set; }

    public string FullName { get; set; } = null!;

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual ICollection<SubLesson> SubLessons { get; set; } = new List<SubLesson>();
}
