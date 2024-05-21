namespace Schedule.Core.Common.Interfaces;

public interface IDayInfoService
{
    int CurrentDayId { get; }

    int GetDayId(DateTime dateTime);
    int GetDayId(DateOnly dateOnly);
}