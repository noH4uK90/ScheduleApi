using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Teachers.Commands.Create;

public sealed class CreateTeacherCommand : IRequest<int>, IMapWith<Teacher>
{
    public required string Name { get; set; }
    
    public required string Surname { get; set; }
    
    public string? MiddleName { get; set; }
    
    public required string Email { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<CreateTeacherCommand, Teacher>()
            .ForMember(teacher => teacher.Account, expression =>
                expression.MapFrom(command => new Account
                {
                    Name = command.Name,
                    Surname = command.Surname,
                    MiddleName = command.MiddleName,
                    Email = command.Email,
                    RoleId = 3
                }));

        profile.CreateMap<Teacher, CreateTeacherCommand>()
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