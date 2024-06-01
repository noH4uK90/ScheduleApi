using AutoMapper;
using MediatR;
using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Teachers.Commands.Update;

public sealed class UpdateTeacherCommand : IRequest<Unit>, IMapWith<Teacher>
{
    public required int Id { get; set; }
    
    public required string Name { get; set; }
    
    public required string Surname { get; set; }
    
    public string? MiddleName { get; set; }
    
    public required string Email { get; set; }
    
    public void Map(Profile profile)
    {
        profile.CreateMap<UpdateTeacherCommand, Teacher>()
            .ForMember(teacher => teacher.Account, expression =>
                expression.MapFrom(command => new Account
                {
                    Name = command.Name,
                    Surname = command.Surname,
                    MiddleName = command.MiddleName,
                    Email = command.Email,
                    RoleId = (int)AccountRole.Teacher
                }));

        profile.CreateMap<Teacher, UpdateTeacherCommand>()
            .ForMember(command => command.Id, expression =>
                expression.MapFrom(teacher => teacher.TeacherId))
            .ForMember(command => command.Name, expression =>
                expression.MapFrom(teacher => teacher.Account.Email))
            .ForMember(command => command.Surname, expression =>
                expression.MapFrom(teacher => teacher.Account.Surname))
            .ForMember(command => command.MiddleName, expression =>
                expression.MapFrom(teacher => teacher.Account.MiddleName))
            .ForMember(command => command.Email, expression =>
                expression.MapFrom(teacher => teacher.Account.Email));
    }
}