using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Students.Commands.Create;

public sealed class CreateStudentCommand : IRequest<int>, IMapWith<Student>
{
    public required string Name { get; set; }
    
    public required string Surname { get; set; }
    
    public string? MiddleName { get; set; }
    
    public required string Email { get; set; }
    
    public required int GroupId { get; set; }
    
    public void Map(Profile profile)
    {
        profile.CreateMap<CreateStudentCommand, Student>()
            .ForMember(student => student.Account, expression =>
                expression.MapFrom(command => new Account
                {
                    Name = command.Name,
                    Surname = command.Surname,
                    MiddleName = command.MiddleName,
                    Email = command.Email,
                    RoleId = 4
                }))
            .ForMember(student => student.GroupId, expression =>
                expression.MapFrom(command => command.GroupId));

        profile.CreateMap<Student, CreateStudentCommand>()
            .ForMember(command => command.Name, expression =>
                expression.MapFrom(student => student.Account.Name))
            .ForMember(command => command.Surname, expression =>
                expression.MapFrom(student => student.Account.Surname))
            .ForMember(command => command.MiddleName, expression =>
                expression.MapFrom(student => student.Account.MiddleName))
            .ForMember(command => command.Email, expression =>
                expression.MapFrom(student => student.Account.Email))
            .ForMember(command => command.GroupId, expression =>
                expression.MapFrom(student => student.GroupId));
    }
}