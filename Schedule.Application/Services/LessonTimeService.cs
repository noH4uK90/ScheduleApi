using Schedule.Application.Common.Interfaces;

namespace Schedule.Application.Services;

public class LessonTimeService : ILessonTimeService
{
    public string GetTimeStart(int dayId, int number)
    {
        return number switch
        {
            0 => dayId == 1 ? "8:30" : "",
            1 => dayId == 1 ? "9:05" : "8:30",
            2 => dayId == 1 ? "10:55" : "10:20",
            3 => dayId == 1 ? "13:15" : "12:40",
            4 => dayId == 1 ? "15:05" : "14:30",
            5 => dayId == 1 ? "16:55" : "16:20",
            6 => dayId == 1 ? "18:45" : "18:10",
            _ => ""
        };
    }

    public string GetTimeEnd(int dayId, int number)
    {
        return number switch
        {
            0 => dayId == 1 ? "8:50" : "",
            1 => dayId == 1 ? "10:45" : "10:10",
            2 => dayId == 1 ? "12:35" : "12:00",
            3 => dayId == 1 ? "14:55" : "14:20",
            4 => dayId == 1 ? "16:45" : "16:10",
            5 => dayId == 1 ? "18:35" : "18:00",
            6 => dayId == 1 ? "20:25" : "19:50",
            _ => ""
        };
    }
}