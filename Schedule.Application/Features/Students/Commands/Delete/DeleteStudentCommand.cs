using MediatR;

namespace Schedule.Application.Features.Students.Commands.Delete;

public sealed record DeleteStudentCommand(int Id) : IRequest<Unit>;