using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Employees.Commands.Create;

public sealed class CreateEmployeeCommand : IRequest<int>, IMapWith<Employee>
{
    public required string Name { get; set; }
    
    public required string Surname { get; set; }
    
    public string? MiddleName { get; set; }
    
    public required string Email { get; set; }
    
    public void Map(Profile profile)
    {
        profile.CreateMap<CreateEmployeeCommand, Employee>()
            .ForMember(employee => employee.Account, expression =>
                expression.MapFrom(command => new Account
                {
                    Name = command.Name,
                    Surname = command.Surname,
                    MiddleName = command.MiddleName,
                    Email = command.Email,
                    RoleId = 2
                }));

        profile.CreateMap<Employee, CreateEmployeeCommand>()
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