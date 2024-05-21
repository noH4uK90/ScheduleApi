using Schedule.Core.Models;

namespace Schedule.Application.Common.Interfaces;

public interface IAktScheduleClient
{
    Task<ICollection<ParsedScheduleItem>> GetSchedule(int groupId);
    Task<ICollection<ParsedScheduleItem>> GetSchedule(int groupId, DateOnly date);
    Task<ICollection<Group>> GetGroups();
}