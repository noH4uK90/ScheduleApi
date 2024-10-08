using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class TeacherFullNameViewModel : IMapWith<TeacherFullName>
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;
    
    public void Map(Profile profile)
    {
        profile.CreateMap<TeacherFullName, TeacherFullNameViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(teacher => teacher.FullNameId))
            .ReverseMap();
    }
}