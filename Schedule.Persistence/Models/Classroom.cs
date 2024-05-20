using System;
using System.Collections.Generic;

namespace Schedule.Persistence.Models;

public partial class Classroom
{
    public int ClassroomId { get; set; }

    public string Cabinet { get; set; } = null!;

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
