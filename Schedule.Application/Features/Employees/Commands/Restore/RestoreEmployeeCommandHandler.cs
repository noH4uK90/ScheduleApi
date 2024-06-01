using MediatR;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Employees.Commands.Restore;

public sealed class RestoreEmployeeCommandHandler(IEmployeeRepository employeeRepository)
    : IRequestHandler<RestoreEmployeeCommand, Unit>
{
    public async Task<Unit> Handle(RestoreEmployeeCommand request, CancellationToken cancellationToken)
    {
        await employeeRepository.RestoreAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}