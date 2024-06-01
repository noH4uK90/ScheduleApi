using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;

namespace Schedule.Application.Features.Discipline.Queries.GetList;

public class GetDisciplinesListQueryHandler(
    IScheduleDbContext context,
    IMapper mapper) 
    : IRequestHandler<GetDisciplinesListQuery, ICollection<DisciplineViewModel>>
{
    public async Task<ICollection<DisciplineViewModel>> Handle(GetDisciplinesListQuery request, CancellationToken cancellationToken)
    {
        var query = context.Disciplines
            .AsNoTracking()
            .AsSplitQuery();

        if (request.Search is not null)
        {
            query = query
                .Where(e => e.Name.Contains(request.Search));
        }

        return await query
            .ProjectTo<DisciplineViewModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}