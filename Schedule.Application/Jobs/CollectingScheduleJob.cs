using System.Diagnostics.CodeAnalysis;
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
                var scheduleDays = schedules.GroupBy(e => e.DayId);

                foreach (var day in scheduleDays)
                {
                    var date = startOfWeek.AddDays(day.Key - 1);
                    var timetable = await timetableRepository.AddIfNotExists(new Timetable
                    {
                        Date = date,
                        GroupId = group.GroupId,
                        DayId = day.Key
                    });
                    await lessonRepository.DeleteAsync(timetable.TimetableId);

                    foreach (var lesson in day)
                    {
                        var timeStart = lessonTimeService.GetTimeStart(day.Key, lesson.Number);
                        var timeEnd = lessonTimeService.GetTimeEnd(day.Key, lesson.Number);

                        await lessonRepository.AddAsync(timetable.TimetableId, lesson, timeStart, timeEnd);
                    }

                    await context.SaveChangesAsync();
                }
            }
    }
}