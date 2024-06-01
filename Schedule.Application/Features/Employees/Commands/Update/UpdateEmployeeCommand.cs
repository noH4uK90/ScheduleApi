using AutoMapper;
using MediatR;
using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Employees.Commands.Update;

public sealed class UpdateEmployeeCommand : IRequest<Unit>, IMapWith<Employee>
{
    public required int Id { get; set; }
    
    public required string Name { get; set; }
    
    public required string Surname { get; set; }
    
    public string? MiddleName { get; set; }
    
    public required string Email { get; set; }
    
    public void Map(Profile profile)
    {
        profile.CreateMap<UpdateEmployeeCommand, Employee>()
            .ForMember(employee => employee.EmployeeId, expression =>
                expression.MapFrom(command => command.Id))
            .ForMember(employee => employee.Account, expression =>
                expression.MapFrom(command => new Account
                {
                    Name = command.Name,
                    Surname = command.Surname,
                    MiddleName = command.MiddleName,
                    Email = command.Email,
                    RoleId = (int)AccountRole.Employee
                }));

        profile.CreateMap<Employee, UpdateEmployeeCommand>()
            .ForMember(command => command.Id, expression =>
                expression.MapFrom(employee => employee.EmployeeId))
            .ForMember(command => command.Name, expression =>
                expression.MapFrom(employee => employee.Account.Name))
            .ForMember(command => command.Surname, expression =>
                expression.MapFrom(employee => employee.Account.Surname))
            .ForMember(command => command.MiddleName, expression =>
                expression.MapFrom(employee => employee.Account.MiddleName))
            .ForMember(command => command.Email, expression =>
                expression.MapFrom(employee => employee.Account.Email));
    }
}