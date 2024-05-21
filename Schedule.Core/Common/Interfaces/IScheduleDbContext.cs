using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Schedule.Core.Models;


namespace Schedule.Core.Common.Interfaces;

public interface IScheduleDbContext
{
    DatabaseFacade Database { get; }
    
    DbSet<Account> Accounts { get; }

    DbSet<Classroom> Classrooms { get; }

    DbSet<Day> Days { get; }

    DbSet<Discipline> Disciplines { get; }

    DbSet<Employee> Employees { get; }

    DbSet<Group> Groups { get; }

    DbSet<Homework> Homeworks { get; }
    
    DbSet<LessonType> LessonTypes { get; }

    DbSet<Lesson> Lessons { get; }

    DbSet<MiddleName> MiddleNames { get; }

    DbSet<Name> Names { get; }

    DbSet<Notification> Notifications { get; }

    DbSet<Role> Roles { get; }

    DbSet<Session> Sessions { get; }

    DbSet<Student> Students { get; }
    
    DbSet<SubLesson> SubLessons { get; }

    DbSet<Surname> Surnames { get; }

    DbSet<Teacher> Teachers { get; }

    DbSet<TeacherFullName> TeacherFullNames { get; }

    DbSet<Timetable> Timetables { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task WithTransactionAsync(Func<Task> action, CancellationToken cancellationToken = default);

    Task<T> WithTransactionAsync<T>(Func<Task<T>> action, CancellationToken cancellationToken = default);
}