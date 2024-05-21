namespace Schedule.Core.Models;

public partial class Session
{
    public int AccountId { get; set; }

    public string RefreshToken { get; set; } = null!;

    public DateTime Created { get; set; }

    public DateTime? Updated { get; set; }

    public Guid SessionId { get; set; }

    public virtual Account Account { get; set; } = null!;
}
