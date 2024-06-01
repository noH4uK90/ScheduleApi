using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;

namespace Schedule.Application.Features.Groups.Queries.GetList;

public class GetGroupListQueryHandler(
    IScheduleDbContext context,
    IMapper mapper) : IRequestHandler<GetGroupListQuery, ICollection<GroupViewModel>>
{
    public async Task<ICollection<GroupViewModel>> Handle(GetGroupListQuery request, CancellationToken cancellationToken)
    {
        var query = context.Groups
            .AsSplitQuery()
            .AsNoTracking();

        if (request.Search is not null)
        {
            query = query
                .Where(e => e.Name.Contains(request.Search));
        }

        return await query
            .ProjectTo<GroupViewModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}