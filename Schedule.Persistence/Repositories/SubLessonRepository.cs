using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class SubLessonRepository(
    IScheduleDbContext context,
    IClassroomRepository classroomRepository,
    IDisciplineRepository disciplineRepository,
    ITeacherFullNameRepository teacherRepository,
    ILessonTypeRepository lessonTypeRepository) : ISubLessonRepository
{
    public async Task<int> AddIfNotExists(ParsedScheduleSubItem subLesson, CancellationToken cancellationToken = default)
    {
        var disciplineDb = await disciplineRepository.AddIfNotExists(subLesson.Discipline, cancellationToken);
        var typeDb = await lessonTypeRepository.AddIfNotExists(subLesson.Type, cancellationToken);
        var teacherDb = await teacherRepository.AddIfNotExists(subLesson.Teacher, cancellationToken);
        var classroomDb = await classroomRepository.AddIfNotExists(subLesson.Classroom, cancellationToken);

        var subLessonDb = await context.SubLessons
            .FirstOrDefaultAsync(e =>
                e.DisciplineId == disciplineDb.DisciplineId &&
                e.TeacherId == teacherDb.FullNameId &&
                e.ClassroomId == classroomDb.ClassroomId &&
                e.TypeId == typeDb.TypeId, cancellationToken);

        if (subLessonDb is not null) return subLessonDb.SubLessonId;

        var created = await context.SubLessons
            .AddAsync(new SubLesson
            {
                DisciplineId = disciplineDb.DisciplineId,
                ClassroomId = classroomDb.ClassroomId,
                TeacherId = teacherDb.FullNameId,
                TypeId = typeDb.TypeId,
            }, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return created.Entity.SubLessonId;
    }

    public async Task UpdateAsync(int subLessonId, ParsedScheduleSubItem subLesson, CancellationToken cancellationToken = default)
    {
        var subLessonDb = await context.SubLessons
            .FirstOrDefaultAsync(e =>
                e.SubLessonId == subLessonId, cancellationToken);

        if (subLessonDb is null)
        {
            throw new NotFoundException(nameof(SubLesson));
        }
        
        var disciplineDb = await disciplineRepository.AddIfNotExists(subLesson.Discipline, cancellationToken);
        var typeDb = await lessonTypeRepository.AddIfNotExists(subLesson.Type, cancellationToken);
        var teacherDb = await teacherRepository.AddIfNotExists(subLesson.Teacher, cancellationToken);
        var classroomDb = await classroomRepository.AddIfNotExists(subLesson.Classroom, cancellationToken);

        subLessonDb.DisciplineId = disciplineDb.DisciplineId;
        subLessonDb.TeacherId = teacherDb.FullNameId;
        subLessonDb.ClassroomId = classroomDb.ClassroomId;
        subLessonDb.TypeId = typeDb.TypeId;
        
        context.SubLessons.Update(subLessonDb);
        await context.SaveChangesAsync(cancellationToken);
    }
}