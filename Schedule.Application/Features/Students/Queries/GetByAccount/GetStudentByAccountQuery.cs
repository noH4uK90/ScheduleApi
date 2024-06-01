using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Students.Queries.GetByAccount;

public sealed record GetStudentByAccountQuery(int Id) : IRequest<StudentViewModel>;