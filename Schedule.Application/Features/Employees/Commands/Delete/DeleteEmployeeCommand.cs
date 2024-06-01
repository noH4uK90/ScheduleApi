using MediatR;

namespace Schedule.Application.Features.Employees.Commands.Delete;

public sealed record DeleteEmployeeCommand(int Id) : IRequest<Unit>;