using System.Globalization;
using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class LessonViewModel : IMapWith<Lesson>
{
    public int Id { get; set; }

    public int Number { get; set; }

    public DisciplineViewModel Discipline { get; set; } = null!;
    
    public TeacherFullNameViewModel? Teacher { get; set; }
    
    public ClassroomViewModel? Classroom { get; set; }

    public LessonTypeViewModel Type { get; set; } = null!;
    
    public SubLessonViewModel? SubLesson { get; set; }

    public string TimeStart { get; set; } = null!;

    public string TimeEnd { get; set; } = null!;

    public void Map(Profile profile)
    {
        profile.CreateMap<Lesson, LessonViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(lesson => lesson.LessonId))
            .ReverseMap();

        profile.CreateMap<LessonViewModel, Lesson>()
            .ForMember(lesson => lesson.LessonId, expression =>
                expression.MapFrom(viewModel => viewModel.Id))
            .ForMember(lesson => lesson.TimeStart, expression =>
                expression.MapFrom(viewModel => TimeOnly.Parse(viewModel.TimeStart ?? "00:00", CultureInfo.InvariantCulture)))
            .ForMember(lesson => lesson.TimeEnd, expression =>
                expression.MapFrom(viewModel => TimeOnly.Parse(viewModel.TimeEnd ?? "00:00", CultureInfo.InvariantCulture)))
            .ReverseMap();
    }
}