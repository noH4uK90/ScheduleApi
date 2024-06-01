namespace Schedule.Core.Models;

public class PagedList<T>
{
    public int PageSize { get; set; }

    public int PageNumber { get; set; }

    public int TotalCount { get; set; }

    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

    public ICollection<T> Items { get; set; } = null!;
}