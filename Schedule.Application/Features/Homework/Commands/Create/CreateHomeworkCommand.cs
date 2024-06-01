using System.Globalization;
using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;

namespace Schedule.Application.Features.Homework.Commands.Create;

public record CreateHomeworkCommand : IRequest<int>, IMapWith<Core.Models.Homework>
{
    public string Title { get; set; }
    
    public string? Description { get; set; }
    
    public int TeacherId { get; set; }
    
    public int GroupId { get; set; }
    
    public int DisciplineId { get; set; }
    
    public string? Expires { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<CreateHomeworkCommand, Core.Models.Homework>()
            .ForMember(homework => homework.Created, expression =>
                expression.MapFrom(command => DateTime.Now))
            .ForMember(homework => homework.Expires, expression =>
                expression.MapFrom(command => command.Expires != null ? DateTime.ParseExact(command.Expires, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) : DateTime.Now));
    }
}