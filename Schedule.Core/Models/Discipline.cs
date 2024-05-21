﻿namespace Schedule.Core.Models;

public partial class Discipline
{
    public int DisciplineId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual ICollection<SubLesson> SubLessons { get; set; } = new List<SubLesson>();
}
