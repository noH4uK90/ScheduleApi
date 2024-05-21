using Microsoft.EntityFrameworkCore;
using Quartz;
using Schedule.Application.Common.Interfaces;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Jobs;

public class CollectingNextWeekScheduleJob(
    IAktScheduleClient client,
    IScheduleDbContext context,
    IDateInfoService dateInfoService,
    ILessonTimeService lessonTimeService,
    ILessonRepository lessonRepository,
    ITimetableRepository timetableRepository)
    : IJob
{
    public async Task Execute(IJobExecutionContext context1)
    {
        var groups = await context.Groups.ToListAsync();
        var startOfWeek = dateInfoService.GetStartOfWeek(DateTime.Now);
        var startOfNextWeek = startOfWeek;//.AddDays(7);

        foreach (var group in groups)
        {
            var schedules = await client.GetSchedule(group.GroupId, startOfNextWeek);

            foreach (var schedule in schedules)
            {
                await context.WithTransactionAsync(async () =>
                {
                    var date = startOfNextWeek.AddDays(schedule.DayId - 1);

                    var timeStart = lessonTimeService.GetTimeStart(schedule.DayId, schedule.Number);
                    var timeEnd = lessonTimeService.GetTimeEnd(schedule.DayId, schedule.Number);
                    
                    var timetable = await timetableRepository.AddIfNotExists(new Timetable
                    {
                        Date = date,
                        GroupId = group.GroupId,
                        DayId = schedule.DayId
                    });
                    
                    await lessonRepository.AddAsync(timetable.TimetableId, schedule, timeStart, timeEnd);

                    await context.SaveChangesAsync();
                });
            }
        }
    }
}