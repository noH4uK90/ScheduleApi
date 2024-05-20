using System;
using System.Collections.Generic;

namespace Schedule.Persistence.Models;

public partial class LessonType
{
    public int TypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
