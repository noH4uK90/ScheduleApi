using Schedule.Application.ViewModels;

namespace Schedule.Application.Common.EqualityComparers;

public sealed class GroupViewModelByIdEqualityComparer : IEqualityComparer<GroupViewModel>
{
    public bool Equals(GroupViewModel x, GroupViewModel y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.Id == y.Id;
    }

    public int GetHashCode(GroupViewModel obj)
    {
        return HashCode.Combine(obj.Id, obj.Name);
    }
}