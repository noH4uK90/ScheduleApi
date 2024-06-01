using AutoMapper;
using MediatR;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Employees.Commands.Update;

public sealed class UpdateEmployeeCommandHandler(
    IEmployeeRepository employeeRepository,
    IMapper mapper) : IRequestHandler<UpdateEmployeeCommand, Unit>
{
    public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = mapper.Map<Employee>(request);
        await employeeRepository.UpdateAsync(employee, cancellationToken);
        return Unit.Value;
    }
}