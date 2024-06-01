using MediatR;
using Schedule.Application.ViewModels;


namespace Schedule.Application.Features.Employees.Queries.GetEmployeeByAccount;

public record GetEmployeeByAccountQuery(int Id) : IRequest<EmployeeViewModel>;