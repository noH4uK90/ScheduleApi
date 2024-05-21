namespace Schedule.Core.Models;

public partial class Teacher
{
    public int TeacherId { get; set; }

    public int AccountId { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Homework> Homeworks { get; set; } = new List<Homework>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
