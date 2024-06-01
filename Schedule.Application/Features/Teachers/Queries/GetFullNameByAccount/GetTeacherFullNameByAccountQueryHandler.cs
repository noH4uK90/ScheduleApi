using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Teachers.Queries.GetFullNameByAccount;

public class GetTeacherFullNameByAccountQueryHandler(
    IScheduleDbContext context,
    IMapper mapper)
    : IRequestHandler<GetTeacherFullNameByAccountQuery, TeacherFullNameViewModel>
{
    public async Task<TeacherFullNameViewModel> Handle(GetTeacherFullNameByAccountQuery request, CancellationToken cancellationToken)
    {
        var teacherDb = await context.Teachers
            .Include(e => e.Account)
            .FirstOrDefaultAsync(e => e.AccountId == request.AccountId, cancellationToken);

        if (teacherDb is null)
        {
            throw new NotFoundException(nameof(Teacher), request.AccountId);
        }

        var teacherFullNameDb = await context.TeacherFullNames
            .FirstOrDefaultAsync(e => e.FullName == GetShortFullName(teacherDb), cancellationToken);

        if (teacherFullNameDb is null)
        {
            throw new NotFoundException(nameof(TeacherFullName), request.AccountId);
        }

        return mapper.Map<TeacherFullNameViewModel>(teacherFullNameDb);
    }

    private static string GetShortFullName(Teacher teacher)
    {
        var result = $"{teacher.Account.Surname} {teacher.Account.Name[0]}.";
        if (teacher.Account.MiddleName is not null)
        {
            result += $"{teacher.Account.MiddleName[0]}.";
        }

        return result;
    }
}