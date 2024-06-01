using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Students.Queries.GetByAccount;

public class GetStudentByAccountQueryHandler(IScheduleDbContext context, IMapper mapper) 
    : IRequestHandler<GetStudentByAccountQuery, StudentViewModel>
{
    public async Task<StudentViewModel> Handle(GetStudentByAccountQuery request, CancellationToken cancellationToken)
    {
        var student = await context.Students
            .Include(e => e.Account)
            .ThenInclude(e => e.Role)
            .Include(e => e.Group)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.AccountId == request.Id, cancellationToken);

        if (student is null)
        {
            throw new NotFoundException(nameof(Student), request.Id);
        }

        return mapper.Map<StudentViewModel>(student);
    }
}