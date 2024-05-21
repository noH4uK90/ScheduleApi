using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class LessonRepository(
    IScheduleDbContext context,
    IClassroomRepository classroomRepository,
    IDisciplineRepository disciplineRepository,
    ITeacherFullNameRepository teacherRepository,
    ILessonTypeRepository lessonTypeRepository,
    ISubLessonRepository subLessonRepository)
    : ILessonRepository
{
    public async Task AddAsync(int timetableId, ParsedScheduleItem lesson, string timeStart, string timeEnd, CancellationToken cancellationToken = default)
    {
        await context.WithTransactionAsync(async () =>
        {
            var timetable = await context.Timetables
                .FirstOrDefaultAsync(e => e.TimetableId == timetableId, cancellationToken);

            if (timetable is null)
            {
                throw new NotFoundException(nameof(Timetable), timetableId);
            }
            
            var disciplineDb = await disciplineRepository.AddIfNotExists(lesson.Discipline, cancellationToken);
            var typeDb = await lessonTypeRepository.AddIfNotExists(lesson.Type, cancellationToken);
            var teacherDb = await teacherRepository.AddIfNotExists(lesson.Teacher, cancellationToken);
            var classroomDb = await classroomRepository.AddIfNotExists(lesson.Classroom, cancellationToken);

            int? subLessonId = null;

            if (lesson.SubItem is not null)
            {
                subLessonId = await subLessonRepository.AddIfNotExists(lesson.SubItem, cancellationToken);
            }

            await context.Lessons
                .AddAsync(new Lesson
                {
                    TimetableId = timetableId,
                    Number = lesson.Number,
                    DisciplineId = disciplineDb.DisciplineId,
                    TypeId = typeDb.TypeId,
                    TeacherId = teacherDb.FullNameId,
                    ClassroomId = classroomDb.ClassroomId,
                    TimeStart = timeStart,
                    TimeEnd = timeEnd,
                    SubLessonId = subLessonId
                }, cancellationToken);
        }, cancellationToken);
    }

    public async Task UpdateAsync(int timetableId, ParsedScheduleItem lesson, string timeStart, string timeEnd,
        CancellationToken cancellationToken = default)
    {
        await context.WithTransactionAsync(async () =>
        {
            var lessonDb = await context.Lessons
                .FirstOrDefaultAsync(e =>
                    e.TimetableId == timetableId &&
                    e.Number == lesson.Number, cancellationToken);

            if (lessonDb is null)
            {
                throw new NotFoundException(nameof(Lesson));
            }
            
            var disciplineDb = await disciplineRepository.AddIfNotExists(lesson.Discipline, cancellationToken);
            var typeDb = await lessonTypeRepository.AddIfNotExists(lesson.Type, cancellationToken);
            var teacherDb = await teacherRepository.AddIfNotExists(lesson.Teacher, cancellationToken);
            var classroomDb = await classroomRepository.AddIfNotExists(lesson.Classroom, cancellationToken);

            if (lesson.SubItem is null)
            {
                lessonDb.SubLessonId = null;
            }
            else
            {
                if (lessonDb.SubLessonId is not null)
                {
                    await subLessonRepository.UpdateAsync(lessonDb.SubLessonId.Value, lesson.SubItem, cancellationToken);
                }
                else
                {
                    await subLessonRepository.AddIfNotExists(lesson.SubItem, cancellationToken);
                }
            }
            
            lessonDb.DisciplineId = disciplineDb.DisciplineId;
            lessonDb.TeacherId = teacherDb.FullNameId;
            lessonDb.ClassroomId = classroomDb.ClassroomId;
            lessonDb.TypeId = typeDb.TypeId;

            context.Lessons
                .Update(lessonDb);
            await context.SaveChangesAsync(cancellationToken);
        }, cancellationToken);
    }
}