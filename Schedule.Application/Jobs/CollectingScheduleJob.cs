using Microsoft.EntityFrameworkCore;
using Quartz;
using Schedule.Application.Common.Interfaces;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Jobs;

public class CollectingScheduleJob(
    IAktScheduleClient client,
    IScheduleDbContext context,
    IDateInfoService dateInfoService,
    ILessonTimeService lessonTimeService,
    ILessonRepository lessonRepository,
    ITimetableRepository timetableRepository)
    : IJob
{
    public async Task Execute(IJobExecutionContext jobContext)
    {
        var groups = await context.Groups.ToListAsync();
        var startOfWeek = dateInfoService.GetStartOfWeek(DateTime.Now);

        foreach (var group in groups)
        {
            var schedules = await client.GetSchedule(group.GroupId, startOfWeek);

            foreach (var schedule in schedules)
            {
                await context.WithTransactionAsync(async () =>
                {
                    var date = startOfWeek.AddDays(schedule.DayId - 1);

                    var timeStart = lessonTimeService.GetTimeStart(schedule.DayId, schedule.Number);
                    var timeEnd = lessonTimeService.GetTimeEnd(schedule.DayId, schedule.Number);
                    
                    var timetable = await timetableRepository.AddIfNotExists(new Timetable
                    {
                        Date = date,
                        GroupId = group.GroupId,
                        DayId = schedule.DayId
                    });
                    
                    await lessonRepository.UpdateAsync(timetable.TimetableId, schedule, timeStart, timeEnd);

                    await context.SaveChangesAsync();
                });
            }
        }
    }
}