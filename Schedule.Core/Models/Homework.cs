﻿namespace Schedule.Core.Models;

public partial class Homework
{
    public int HomeworkId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int TeacherId { get; set; }

    public int GroupId { get; set; }
    
    public int DisciplineId { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Expires { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual Teacher Teacher { get; set; } = null!;

    public virtual Discipline Discipline { get; set; } = null!;
}
