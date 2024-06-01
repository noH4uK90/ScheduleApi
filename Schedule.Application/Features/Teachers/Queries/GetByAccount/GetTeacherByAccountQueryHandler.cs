using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Teachers.Queries.GetByAccount;

public class GetTeacherByAccountQueryHandler(IScheduleDbContext context, IMapper mapper)
    : IRequestHandler<GetTeacherByAccountQuery, TeacherViewModel>
{
    public async Task<TeacherViewModel> Handle(GetTeacherByAccountQuery request, CancellationToken cancellationToken)
    {
        var teacher = await context.Teachers
            .Include(e => e.Account)
            .ThenInclude(e => e.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.AccountId == request.Id, cancellationToken);

        if (teacher is null)
        {
            throw new NotFoundException(nameof(Teacher), request.Id);
        }

        return mapper.Map<TeacherViewModel>(teacher);
    }
}