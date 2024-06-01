using AutoMapper;
using MediatR;
using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Students.Commands.Update;

public sealed class UpdateStudentCommand : IRequest<Unit>, IMapWith<Student>
{
    public required int Id { get; set; }
    
    public required string Name { get; set; }
    
    public required string Surname { get; set; }
    
    public string? MiddleName { get; set; }
    
    public required string Email { get; set; }
    
    public int? GroupId { get; set; }
    
    public void Map(Profile profile)
    {
        profile.CreateMap<UpdateStudentCommand, Student>()
            .ForMember(student => student.StudentId, expression =>
                expression.MapFrom(command => command.Id))
            .ForMember(student => student.GroupId, expression =>
                expression.MapFrom(command => command.GroupId))
            .ForMember(student => student.Account, expression =>
                expression.MapFrom(command => new Account
                {
                    Name = command.Name,
                    Surname = command.Surname,
                    MiddleName = command.MiddleName,
                    Email = command.Email,
                    RoleId = (int)AccountRole.Student
                }));

        profile.CreateMap<Student, UpdateStudentCommand>()
            .ForMember(command => command.Id, expression =>
                expression.MapFrom(student => student.GroupId))
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