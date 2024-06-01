using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Teachers.Queries.GetFullNameList;

public sealed record GetTeacherFullNameListQuery : IRequest<ICollection<TeacherFullNameViewModel>>
{
    public string? Search { get; set; }
}