using MediatR;

namespace Schedule.Application.Features.Employees.Commands.Restore;

public sealed record RestoreEmployeeCommand(int Id) : IRequest<Unit>;