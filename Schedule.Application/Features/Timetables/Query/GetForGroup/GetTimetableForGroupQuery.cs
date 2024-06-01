using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Timetables.Query.GetForGroup;

public sealed record GetTimetableForGroupQuery : IRequest<ICollection<TimetableViewModel>>
{
    public required int GroupId { get; set; }
    public required DateOnly Date { get; set; }
}