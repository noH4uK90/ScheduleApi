using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Persistence.Context;

public partial class ScheduleDbContext : DbContext, IScheduleDbContext
{
    public ScheduleDbContext()
    {
    }

    public ScheduleDbContext(DbContextOptions<ScheduleDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Classroom> Classrooms { get; set; }

    public virtual DbSet<Day> Days { get; set; }

    public virtual DbSet<Discipline> Disciplines { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Group> Groups { get; set; }
    
    public virtual DbSet<GroupDiscipline> GroupDisciplines { get; set; }

    public virtual DbSet<Homework> Homeworks { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<LessonType> LessonTypes { get; set; }

    public virtual DbSet<MiddleName> MiddleNames { get; set; }

    public virtual DbSet<Name> Names { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<SubLesson> SubLessons { get; set; }

    public virtual DbSet<Surname> Surnames { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<TeacherFullName> TeacherFullNames { get; set; }

    public virtual DbSet<Timetable> Timetables { get; set; }

    public async Task WithTransactionAsync(Func<Task> action, CancellationToken cancellationToken = default)
    {
        if (Database.CurrentTransaction != null)
        {
            await action();
        }
        else
        {
            await using var transaction = await Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await action();
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }

    public async Task<T> WithTransactionAsync<T>(Func<Task<T>> action, CancellationToken cancellationToken = default)
    {
        if (Database.CurrentTransaction != null)
        {
            return await action();
        }

        await using var transaction = await Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var result = await action();
            await transaction.CommitAsync(cancellationToken);
            return result;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("account_pk");

            entity.ToTable("account");

            entity.Property(e => e.AccountId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("account_id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Login).HasColumnName("login");
            entity.Property(e => e.MiddleName).HasColumnName("middle_name");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Surname).HasColumnName("surname");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

            entity.HasOne(d => d.MiddleNameNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.MiddleName)
                .HasConstraintName("account_middle_name_fk");

            entity.HasOne(d => d.NameNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.Name)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("account_name_fk");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("account_role_id_fk");

            entity.HasOne(d => d.SurnameNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.Surname)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("account_surname_fk");
        });

        modelBuilder.Entity<Classroom>(entity =>
        {
            entity.HasKey(e => e.ClassroomId).HasName("classroom_pk");

            entity.ToTable("classroom");

            entity.HasIndex(e => e.Cabinet, "classroom_cabinet_index").IsUnique();

            entity.Property(e => e.ClassroomId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("classroom_id");
            entity.Property(e => e.Cabinet).HasColumnName("cabinet");
        });

        modelBuilder.Entity<Day>(entity =>
        {
            entity.HasKey(e => e.DayId).HasName("day_pk");

            entity.ToTable("day");

            entity.Property(e => e.DayId).HasColumnName("day_id");
            entity.Property(e => e.IsStudy)
                .HasDefaultValue(false)
                .HasColumnName("is_study");
            entity.Property(e => e.WeekDay).HasColumnName("week_day");
        });

        modelBuilder.Entity<Discipline>(entity =>
        {
            entity.HasKey(e => e.DisciplineId).HasName("discipline_pk");

            entity.ToTable("discipline");

            entity.HasIndex(e => e.Name, "discipline_name_index").IsUnique();

            entity.Property(e => e.DisciplineId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("discipline_id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("employee_pk");

            entity.ToTable("employee");

            entity.Property(e => e.EmployeeId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("employee_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");

            entity.HasOne(d => d.Account).WithMany(p => p.Employees)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("employee_account_id_fk");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("group_pk");

            entity.ToTable("group");

            entity.HasIndex(e => e.Name, "group_name_index").IsUnique();

            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<GroupDiscipline>(entity =>
        {
            entity.HasKey(e => new { e.GroupId, e.DisciplineId })
                .HasName("group_discipline_pk");

            entity.ToTable("group_discipline");

            entity.Property(e => e.GroupId)
                .HasColumnName("group_id");
            entity.Property(e => e.DisciplineId)
                .HasColumnName("discipline_id");

            entity.HasOne(d => d.Group)
                .WithMany(p => p.GroupDisciplines)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("group_discipline_group_id_fk");

            entity.HasOne(d => d.Discipline)
                .WithMany(p => p.GroupDisciplines)
                .HasForeignKey(d => d.DisciplineId)
                .HasConstraintName("group_discipline_discipline_id_fk");
        });

        modelBuilder.Entity<Homework>(entity =>
        {
            entity.HasKey(e => e.HomeworkId).HasName("homework_pk");

            entity.ToTable("homework");

            entity.Property(e => e.HomeworkId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("homework_id");
            entity.Property(e => e.Created)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Expires)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expires");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.TeacherId).HasColumnName("teacher_id");
            entity.Property(e => e.DisciplineId).HasColumnName("discipline_id");
            entity.Property(e => e.Title).HasColumnName("title");

            entity.HasOne(d => d.Group).WithMany(p => p.Homeworks)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("homework_group_id_fk");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Homeworks)
                .HasForeignKey(d => d.TeacherId)
                .HasConstraintName("homework_teacher_id_fk");

            entity.HasOne(d => d.Discipline).WithMany(p => p.Homeworks)
                .HasForeignKey(d => d.DisciplineId)
                .HasConstraintName("homework_discipline_id_fk");
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.LessonId).HasName("lesson_pk");

            entity.ToTable("lesson");

            entity.HasIndex(e => e.SubLessonId, "lesson_sub_lesson_uq").IsUnique();

            entity.Property(e => e.LessonId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("lesson_id");
            entity.Property(e => e.ClassroomId).HasColumnName("classroom_id");
            entity.Property(e => e.DisciplineId).HasColumnName("discipline_id");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.SubLessonId).HasColumnName("sub_lesson_id");
            entity.Property(e => e.TeacherId).HasColumnName("teacher_id");
            entity.Property(e => e.TimeEnd).HasColumnName("time_end");
            entity.Property(e => e.TimeStart).HasColumnName("time_start");
            entity.Property(e => e.TimetableId).HasColumnName("timetable_id");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Classroom).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.ClassroomId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("lesson_classroom_id_fk");

            entity.HasOne(d => d.Discipline).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.DisciplineId)
                .HasConstraintName("lesson_discipline_id_fk");

            entity.HasOne(d => d.SubLesson).WithOne(p => p.Lesson)
                .HasForeignKey<Lesson>(d => d.SubLessonId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("lesson_sub_lesson_id_fk");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("lesson_teacher_id_fk");

            entity.HasOne(d => d.Timetable).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.TimetableId)
                .HasConstraintName("lesson_timetable_id_fk");

            entity.HasOne(d => d.Type).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("lesson_type_id_fk");
        });

        modelBuilder.Entity<LessonType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("lesson_type_pk");

            entity.ToTable("lesson_type");

            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<MiddleName>(entity =>
        {
            entity.HasKey(e => e.Value).HasName("middle_name_pk");

            entity.ToTable("middle_name");

            entity.Property(e => e.Value).HasColumnName("value");
        });

        modelBuilder.Entity<Name>(entity =>
        {
            entity.HasKey(e => e.Value).HasName("name_pk");

            entity.ToTable("name");

            entity.Property(e => e.Value).HasColumnName("value");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("notification_pk");

            entity.ToTable("notification");

            entity.Property(e => e.NotificationId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("notification_id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.TeacherId).HasColumnName("teacher_id");
            entity.Property(e => e.Title).HasColumnName("title");

            entity.HasOne(d => d.Group).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("notification_group_id_fk");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.TeacherId)
                .HasConstraintName("notification_teacher_id_fk");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("role_pk");

            entity.ToTable("role");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.SessionId).HasName("session_pk");

            entity.ToTable("session");

            entity.Property(e => e.SessionId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("session_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Created)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.RefreshToken).HasColumnName("refresh_token");
            entity.Property(e => e.Updated)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated");

            entity.HasOne(d => d.Account).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("session_account_id_fk");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("student_pk");

            entity.ToTable("student");

            entity.Property(e => e.StudentId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("student_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.GroupId).HasColumnName("group_id");

            entity.HasOne(d => d.Account).WithMany(p => p.Students)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("student_account_id_fk");

            entity.HasOne(d => d.Group).WithMany(p => p.Students)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("student_group_id_fk");
        });

        modelBuilder.Entity<SubLesson>(entity =>
        {
            entity.HasKey(e => e.SubLessonId).HasName("sub_lesson_pk");

            entity.ToTable("sub_lesson");

            entity.Property(e => e.SubLessonId).HasColumnName("sub_lesson_id");
            entity.Property(e => e.ClassroomId).HasColumnName("classroom_id");
            entity.Property(e => e.DisciplineId).HasColumnName("discipline_id");
            entity.Property(e => e.TeacherId).HasColumnName("teacher_id");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Classroom).WithMany(p => p.SubLessons)
                .HasForeignKey(d => d.ClassroomId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("sub_lesson_classroom_id_fk");

            entity.HasOne(d => d.Discipline).WithMany(p => p.SubLessons)
                .HasForeignKey(d => d.DisciplineId)
                .HasConstraintName("sub_lesson_discipline_id_fk");

            entity.HasOne(d => d.Teacher).WithMany(p => p.SubLessons)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("sub_lesson_teacher_id_fk");

            entity.HasOne(d => d.Type).WithMany(p => p.SubLessons)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("sub_lesson_type_id_fk");
        });

        modelBuilder.Entity<Surname>(entity =>
        {
            entity.HasKey(e => e.Value).HasName("surname_pk");

            entity.ToTable("surname");

            entity.Property(e => e.Value).HasColumnName("value");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.TeacherId).HasName("teacher_pk");

            entity.ToTable("teacher");

            entity.Property(e => e.TeacherId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("teacher_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");

            entity.HasOne(d => d.Account).WithMany(p => p.Teachers)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("teacher_account_id_fk");
        });

        modelBuilder.Entity<TeacherFullName>(entity =>
        {
            entity.HasKey(e => e.FullNameId).HasName("teacher_full_name_pk");

            entity.ToTable("teacher_full_name");

            entity.HasIndex(e => e.FullName, "teacher_full_name_full_name_uindex").IsUnique();

            entity.Property(e => e.FullNameId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("full_name_id");
            entity.Property(e => e.FullName).HasColumnName("full_name");
        });

        modelBuilder.Entity<Timetable>(entity =>
        {
            entity.HasKey(e => e.TimetableId).HasName("timetable_pk");

            entity.ToTable("timetable");

            entity.Property(e => e.TimetableId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("timetable_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.DayId).HasColumnName("day_id");
            entity.Property(e => e.GroupId).HasColumnName("group_id");

            entity.HasOne(d => d.Day).WithMany(p => p.Timetables)
                .HasForeignKey(d => d.DayId)
                .HasConstraintName("timetable_day_id_fk");

            entity.HasOne(d => d.Group).WithMany(p => p.Timetables)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("timetable_group_id_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
