using MediatR;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Employees.Commands.UpdatePermissions;

public sealed class EmployeeUpdatePermissionsCommandHandler(IEmployeeRepository employeeRepository)
    : IRequestHandler<EmployeeUpdatePermissionsCommand, Unit>
{
    public async Task<Unit> Handle(EmployeeUpdatePermissionsCommand request, CancellationToken cancellationToken)
    {
        await employeeRepository.UpdatePermissions(request.EmployeeId, request.PermissionIds, cancellationToken);
        return Unit.Value;
    }
}