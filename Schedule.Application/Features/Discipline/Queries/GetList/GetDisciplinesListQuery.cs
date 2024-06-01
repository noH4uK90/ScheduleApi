using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Discipline.Queries.GetList;

public sealed record GetDisciplinesListQuery : IRequest<ICollection<DisciplineViewModel>>
{
    public string? Search { get; set; }
}