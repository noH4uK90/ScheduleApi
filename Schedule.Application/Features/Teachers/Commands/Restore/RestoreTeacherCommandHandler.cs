using MediatR;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Teachers.Commands.Restore;

public sealed class RestoreTeacherCommandHandler(ITeacherRepository teacherRepository)
    : IRequestHandler<RestoreTeacherCommand, Unit>
{
    public async Task<Unit> Handle(RestoreTeacherCommand request, CancellationToken cancellationToken)
    {
        await teacherRepository.RestoreAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}