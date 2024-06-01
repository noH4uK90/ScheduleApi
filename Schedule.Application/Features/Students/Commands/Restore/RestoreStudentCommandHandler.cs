using MediatR;
using Schedule.Application.Features.Students.Commands.Delete;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Students.Commands.Restore;

public sealed class RestoreStudentCommandHandler(IStudentRepository studentRepository)
    : IRequestHandler<RestoreStudentCommand, Unit>
{
    public async Task<Unit> Handle(RestoreStudentCommand request, CancellationToken cancellationToken)
    {
        await studentRepository.RestoreAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}