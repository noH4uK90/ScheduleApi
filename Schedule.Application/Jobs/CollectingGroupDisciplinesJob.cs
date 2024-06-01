using Quartz;
using Schedule.Application.Common.Interfaces;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Jobs;

public class CollectingGroupDisciplinesJob(
    IAktScheduleClient client,
    IScheduleDbContext context,
    IDisciplineRepository disciplineRepository) 
    : IJob
{
    public async Task Execute(IJobExecutionContext jobContext)
    {
        await context.WithTransactionAsync(async () =>
        {
            var groups = await client.GetGroups();

            foreach (var group in groups)
            {
                var schedules = await client.GetSchedule(group.GroupId);
                var disciplines = schedules.Select(e => e.Discipline).Distinct();
                var schedulesNextWeek = await client.GetSchedule(group.GroupId);
            }
        });
    }
}