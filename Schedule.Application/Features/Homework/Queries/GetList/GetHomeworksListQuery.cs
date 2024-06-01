using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Homework.Queries.GetList;

public record GetHomeworksListQuery : IRequest<ICollection<HomeworkViewModel>>
{
    public int GroupId { get; set; }
    
    public int DisciplineId { get; set; }
}