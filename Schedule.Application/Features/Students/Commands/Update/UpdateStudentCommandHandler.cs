using AutoMapper;
using MediatR;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Students.Commands.Update;

public sealed class UpdateStudentCommandHandler(
    IStudentRepository studentRepository,
    IMapper mapper) : IRequestHandler<UpdateStudentCommand, Unit>
{
    public async Task<Unit> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = mapper.Map<Student>(request);
        await studentRepository.UpdateAsync(student, cancellationToken);
        return Unit.Value;
    }
}