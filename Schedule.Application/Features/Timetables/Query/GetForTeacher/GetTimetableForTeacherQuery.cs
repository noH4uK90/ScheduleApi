using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Timetables.Query.GetForTeacher;

public sealed record GetTimetableForTeacherQuery : IRequest<ICollection<LessonViewModel>>
{
    public required int TeacherFullNameId { get; set; }
    public required DateOnly Date { get; set; }
}