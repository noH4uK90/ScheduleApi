using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class LessonTypeViewModel : IMapWith<LessonType>
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public void Map(Profile profile)
    {
        profile.CreateMap<LessonType, LessonTypeViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(type => type.TypeId));
    }
}