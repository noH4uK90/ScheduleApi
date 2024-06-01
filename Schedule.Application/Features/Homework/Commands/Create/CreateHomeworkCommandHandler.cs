using AutoMapper;
using MediatR;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Homework.Commands.Create;

public class CreateHomeworkCommandHandler(
    IHomeworkRepository repository,
    IMapper mapper) 
    : IRequestHandler<CreateHomeworkCommand, int>
{
    public async Task<int> Handle(CreateHomeworkCommand request, CancellationToken cancellationToken)
    {
        var homework = mapper.Map<Core.Models.Homework>(request);
        return await repository.CreateAsync(homework, cancellationToken);
    }
}