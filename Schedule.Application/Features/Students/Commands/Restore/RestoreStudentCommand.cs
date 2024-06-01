using MediatR;

namespace Schedule.Application.Features.Students.Commands.Restore;

public sealed record RestoreStudentCommand(int Id) : IRequest<Unit>;