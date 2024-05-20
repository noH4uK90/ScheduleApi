using System;
using System.Collections.Generic;

namespace Schedule.Persistence.Models;

public partial class TeacherFullName
{
    public int FullNameId { get; set; }

    public string FullName { get; set; } = null!;

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
