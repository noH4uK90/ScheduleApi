using MediatR;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Teachers.Commands.Delete;

public sealed class DeleteTeacherCommandHandler(ITeacherRepository teacherRepository)
    : IRequestHandler<DeleteTeacherCommand, Unit>
{
    public async Task<Unit> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
    {
        await teacherRepository.DeleteAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}