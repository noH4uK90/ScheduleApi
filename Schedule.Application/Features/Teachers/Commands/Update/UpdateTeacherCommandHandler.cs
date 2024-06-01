using AutoMapper;
using MediatR;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Teachers.Commands.Update;

public sealed class UpdateTeacherCommandHandler(
    ITeacherRepository teacherRepository,
    IMapper mapper) : IRequestHandler<UpdateTeacherCommand, Unit>
{
    public async Task<Unit> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
    {
        var teacher = mapper.Map<Teacher>(request);
        await teacherRepository.UpdateAsync(teacher, cancellationToken);
        return Unit.Value;
    }
}