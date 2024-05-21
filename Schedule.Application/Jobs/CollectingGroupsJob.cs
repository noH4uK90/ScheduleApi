using Quartz;
using Schedule.Application.Common.Interfaces;
using Schedule.Core.Common.Interfaces;

namespace Schedule.Application.Jobs;

public class CollectingGroupsJob(
    IAktScheduleClient client,
    IScheduleDbContext context)
    : IJob
{
    public async Task Execute(IJobExecutionContext context1)
    {
        try
        {
            var groups = await client.GetGroups();

            context.Groups.AddRange(groups);
            await context.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            Console.WriteLine($"{exception}");
        }
    }
}