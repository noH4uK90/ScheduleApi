using MediatR;

namespace Schedule.Application.Features.Teachers.Commands.Restore;

public sealed record RestoreTeacherCommand(int Id) : IRequest<Unit>;