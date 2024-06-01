using MediatR;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Employees.Commands.Delete;

public sealed class DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository)
    : IRequestHandler<DeleteEmployeeCommand, Unit>
{
    public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        await employeeRepository.DeleteAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}