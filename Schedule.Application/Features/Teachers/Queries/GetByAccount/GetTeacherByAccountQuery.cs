using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Teachers.Queries.GetByAccount;

public sealed record GetTeacherByAccountQuery(int Id) : IRequest<TeacherViewModel>;