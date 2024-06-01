using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;

namespace Schedule.Application.Features.Homework.Queries.GetList;

public class GetHomeworksListQueryHandler(
    IScheduleDbContext context,
    IMapper mapper) 
    : IRequestHandler<GetHomeworksListQuery, ICollection<HomeworkViewModel>>
{
    public async Task<ICollection<HomeworkViewModel>> Handle(GetHomeworksListQuery request, CancellationToken cancellationToken)
    {
        return await context.Homeworks
            .Include(e => e.Group)
            .Include(e => e.Teacher)
            .Include(e => e.Discipline)
            .Where(e => 
                e.GroupId == request.GroupId &&
                e.DisciplineId == request.DisciplineId)
            .ProjectTo<HomeworkViewModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}