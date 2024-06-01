using MediatR;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Homework.Commands.Delete;

public class DeleteHomeworkCommandHandler(
    IHomeworkRepository repository) 
    : IRequestHandler<DeleteHomeworkCommand, Unit>
{
    public async Task<Unit> Handle(DeleteHomeworkCommand request, CancellationToken cancellationToken)
    {
        await repository.DeleteAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}