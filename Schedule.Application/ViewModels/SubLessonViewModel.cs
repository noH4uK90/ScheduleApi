using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class SubLessonViewModel : IMapWith<SubLesson>
{
    public int Id { get; set; }
    
    public ClassroomViewModel? Classroom { get; set; }

    public DisciplineViewModel Discipline { get; set; } = null!;

    public TeacherFullNameViewModel? Teacher { get; set; }

    public LessonTypeViewModel Type { get; set; } = null!;

    public void Map(Profile profile)
    {
        profile.CreateMap<SubLesson, SubLessonViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(subLesson => subLesson.SubLessonId));
    }
}