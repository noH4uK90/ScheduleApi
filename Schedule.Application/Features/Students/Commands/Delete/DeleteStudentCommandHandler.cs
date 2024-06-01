using MediatR;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Students.Commands.Delete;

public sealed class DeleteStudentCommandHandler(IStudentRepository studentRepository)
    : IRequestHandler<DeleteStudentCommand, Unit>
{
    public async Task<Unit> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        await studentRepository.DeleteAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}