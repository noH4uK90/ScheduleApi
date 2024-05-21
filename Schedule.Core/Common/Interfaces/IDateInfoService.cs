namespace Schedule.Core.Common.Interfaces;

public interface IDateInfoService : IWeekInfoService, IDayInfoService
{
    int CurrentTerm { get; }

    DateTime CurrentDateTime { get; }

    DateOnly CurrentDate { get; }
    TimeOnly CurrentTime { get; }

    int GetTerm(DateTime dateTime);
    int GetTerm(DateOnly dateOnly);

    DateOnly GetStartOfWeek(DateTime date);
}