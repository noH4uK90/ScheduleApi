using System.Globalization;
using Schedule.Core.Common.Interfaces;
using WeekType = Schedule.Core.Common.Enums.WeekType;

namespace Schedule.Application.Services;

public sealed class DateInfoService : IDateInfoService
{
    private readonly Calendar _calendar;
    private readonly DayOfWeek _firstDayOfWeek;

    public DateInfoService()
    {
        var timeFormatInfo = DateTimeFormatInfo.CurrentInfo;
        _calendar = timeFormatInfo.Calendar;
        _firstDayOfWeek = DayOfWeek.Monday;
    }

    public int CurrentTerm => GetTerm(CurrentDateTime);
    public DateTime CurrentDateTime => DateTime.Now;
    public DateOnly CurrentDate => DateOnly.FromDateTime(CurrentDateTime);
    public TimeOnly CurrentTime => TimeOnly.FromDateTime(CurrentDateTime);

    public int CurrentWeekOfYear => GetWeekOfYear(CurrentDateTime);
    public WeekType CurrentWeekType => GetWeekType(CurrentDateTime);
    public int MaxWeekOfYear => GetWeekOfYear(new DateTime(CurrentDateTime.Year, 12, 31));

    public int CurrentDayId => GetDayId(CurrentDateTime);


    public int GetTerm(DateTime dateTime)
    {
        return dateTime.Month < 9 ? 2 : 1;
    }

    public int GetTerm(DateOnly dateOnly)
    {
        return GetTerm(dateOnly.ToDateTime(TimeOnly.MinValue));
    }

    public DateOnly GetStartOfWeek(DateTime date)
    {
        var diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
        var startOfWeek = date.AddDays(-1 * diff).Date;
        return DateOnly.FromDateTime(startOfWeek);
    }

    public int GetWeekOfYear(DateTime dateTime)
    {
        return _calendar.GetWeekOfYear(dateTime,
            CalendarWeekRule.FirstFourDayWeek,
            _firstDayOfWeek);
    }

    public int GetWeekOfYear(DateOnly dateOnly)
    {
        return GetWeekOfYear(dateOnly.ToDateTime(TimeOnly.MinValue));
    }

    public WeekType GetWeekType(DateTime dateTime)
    {
        var yellowWeekDate = dateTime.Month < 9
            ? new DateTime(dateTime.Year, 1, 12)
            : new DateTime(dateTime.Year, 9, 1);
        var yellowWeekIsOdd = IsOddWeek(yellowWeekDate);
        var isOdWeek = IsOddWeek(dateTime);
        return yellowWeekIsOdd == isOdWeek ? WeekType.Yellow : WeekType.Green;
    }

    public WeekType GetWeekType(DateOnly dateOnly)
    {
        return GetWeekType(dateOnly.ToDateTime(TimeOnly.MinValue));
    }

    public int GetDayId(DateTime dateTime)
    {
        var day = (int)dateTime.DayOfWeek;
        return day == 0 ? 7 : day;
    }

    public int GetDayId(DateOnly dateOnly)
    {
        return GetDayId(dateOnly.ToDateTime(TimeOnly.MinValue));
    }

    private bool IsOddWeek(DateTime dateTime)
    {
        var week = GetWeekOfYear(dateTime);
        return (week & 1) != 0;
    }
}