namespace Schedule.Core.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public int AccountId { get; set; }

    public int GroupId { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Group Group { get; set; } = null!;
}
