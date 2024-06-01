using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class HomeworkViewModel : IMapWith<Homework>
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string? Description { get; set; }

    public GroupViewModel Group { get; set; } = null!;

    public TeacherViewModel Teacher { get; set; } = null!;

    public DisciplineViewModel Discipline { get; set; } = null!;
    
    public DateTime Created { get; set; }
    
    public DateTime? Expires { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<Homework, HomeworkViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(homework => homework.HomeworkId))
            .ReverseMap();

        profile.CreateMap<HomeworkViewModel, Homework>()
            .ForMember(homework => homework.HomeworkId, expression =>
                expression.MapFrom(viewModel => viewModel.Id))
            .ReverseMap();
    }
}