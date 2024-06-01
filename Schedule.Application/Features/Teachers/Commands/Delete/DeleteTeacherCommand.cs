using MediatR;

namespace Schedule.Application.Features.Teachers.Commands.Delete;

public sealed record DeleteTeacherCommand(int Id) : IRequest<Unit>;
