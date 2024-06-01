using MediatR;

namespace Schedule.Application.Features.Employees.Commands.UpdatePermissions;

public sealed class EmployeeUpdatePermissionsCommand : IRequest<Unit>
{
    public int EmployeeId { get; set; }
    public int[] PermissionIds { get; set; } = [];
}