using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Teachers.Queries.GetFullNameByAccount;

public record GetTeacherFullNameByAccountQuery(int AccountId) : IRequest<TeacherFullNameViewModel>;