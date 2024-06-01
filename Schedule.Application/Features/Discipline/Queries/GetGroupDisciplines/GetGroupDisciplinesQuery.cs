using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Discipline.Queries.GetGroupDisciplines;

public record GetGroupDisciplinesQuery(int GroupId) : IRequest<ICollection<DisciplineViewModel>>;