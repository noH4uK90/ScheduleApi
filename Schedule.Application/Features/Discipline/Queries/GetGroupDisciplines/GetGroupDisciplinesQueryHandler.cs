using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;

namespace Schedule.Application.Features.Discipline.Queries.GetGroupDisciplines;

public class GetGroupDisciplinesQueryHandler(
    IScheduleDbContext context,
    IMapper mapper) 
    : IRequestHandler<GetGroupDisciplinesQuery, ICollection<DisciplineViewModel>>
{
    public async Task<ICollection<DisciplineViewModel>> Handle(GetGroupDisciplinesQuery request, CancellationToken cancellationToken)
    {
        return await context.GroupDisciplines
            .Include(e => e.Discipline)
            .AsNoTracking()
            .AsSplitQuery()
            .Where(e => e.GroupId == request.GroupId)
            .Select(e => e.Discipline)
            .ProjectTo<DisciplineViewModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}