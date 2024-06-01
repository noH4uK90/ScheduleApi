using MediatR;

namespace Schedule.Application.Features.Homework.Commands.Delete;

public sealed record DeleteHomeworkCommand(int Id) : IRequest<Unit>;