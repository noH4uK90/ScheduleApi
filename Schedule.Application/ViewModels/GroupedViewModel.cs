namespace Schedule.Application.ViewModels;

public class GroupedViewModel<TKey, TItem>
{
    public required TKey Key { get; set; }
    public required ICollection<TItem> Items { get; set; }
}