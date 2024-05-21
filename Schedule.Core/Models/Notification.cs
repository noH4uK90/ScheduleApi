namespace Schedule.Core.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int TeacherId { get; set; }

    public int GroupId { get; set; }

    public DateTime Created { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual Teacher Teacher { get; set; } = null!;
}
