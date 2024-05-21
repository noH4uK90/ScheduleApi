namespace Schedule.Application.Common.Interfaces;

public interface ILessonTimeService
{
    string GetTimeStart(int dayId, int number);
    string GetTimeEnd(int dayId, int number);
}