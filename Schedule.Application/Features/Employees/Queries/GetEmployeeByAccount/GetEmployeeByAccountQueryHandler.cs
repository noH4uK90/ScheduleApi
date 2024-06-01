using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Employees.Queries.GetEmployeeByAccount;

public class GetEmployeeByAccountQueryHandler(IScheduleDbContext context, IMapper mapper) 
    : IRequestHandler<GetEmployeeByAccountQuery, EmployeeViewModel>
{
    public async Task<EmployeeViewModel> Handle(GetEmployeeByAccountQuery request, CancellationToken cancellationToken)
    {
        var employee = await context.Employees
            .Include(e => e.Account)
            .ThenInclude(e => e.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.AccountId == request.Id, cancellationToken);

        if (employee is null)
        {
            throw new NotFoundException(nameof(Employee), request.Id);
        }

        return mapper.Map<EmployeeViewModel>(employee);
    }
}