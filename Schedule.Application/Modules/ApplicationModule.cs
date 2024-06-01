using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using FluentValidation;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using Microsoft.Extensions.DependencyInjection;
using Schedule.Application.Client;
using Schedule.Application.Common.Interfaces;
using Schedule.Application.Common.Mappings;
using Schedule.Application.Services;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Common.Interfaces;
using Schedule.Persistence.Repositories;

namespace Schedule.Application.Modules;

public sealed class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAutoMapper(options =>
            options.AddProfile(new AssemblyMappingProfile(ThisAssembly)));

        builder.RegisterMediatR(MediatRConfigurationBuilder
            .Create(ThisAssembly)
            .WithAllOpenGenericHandlerTypesRegistered()
            .Build());
        
        builder.RegisterType<PasswordGenerator>()
            .As<IPasswordGenerator>()
            .SingleInstance();

        builder.RegisterType<DateInfoService>()
            .As<IDateInfoService>()
            .SingleInstance();

        builder.RegisterType<TokenService>()
            .As<ITokenService>()
            .SingleInstance();

        builder.RegisterType<PasswordHasherService>()
            .As<IPasswordHasherService>()
            .SingleInstance();

        builder.RegisterType<MailSenderService>()
            .As<IMailSenderService>();

        builder.RegisterType<AktScheduleClient>()
            .As<IAktScheduleClient>();

        builder.RegisterType<LessonTimeService>()
            .As<ILessonTimeService>();

        RegisterRepositories(builder);

        var services = new ServiceCollection();

        services.AddValidatorsFromAssembly(ThisAssembly);

        builder.Populate(services);
    }

    private void RegisterRepositories(ContainerBuilder builder)
    {
        builder.RegisterType<AccountRepository>()
            .As<IAccountRepository>();
        
        builder.RegisterType<ClassroomRepository>()
            .As<IClassroomRepository>();

        builder.RegisterType<DisciplineRepository>()
            .As<IDisciplineRepository>();
        
        builder.RegisterType<EmployeeRepository>()
            .As<IEmployeeRepository>();

        builder.RegisterType<HomeworkRepository>()
            .As<IHomeworkRepository>();
        
        builder.RegisterType<LessonRepository>()
            .As<ILessonRepository>();

        builder.RegisterType<LessonTypeRepository>()
            .As<ILessonTypeRepository>();
        
        builder.RegisterType<MiddleNameRepository>()
            .As<IMiddleNameRepository>();
        
        builder.RegisterType<NameRepository>()
            .As<INameRepository>();
        
        builder.RegisterType<SessionRepository>()
            .As<ISessionRepository>();
        
        builder.RegisterType<StudentRepository>()
            .As<IStudentRepository>();

        builder.RegisterType<SubLessonRepository>()
            .As<ISubLessonRepository>();
        
        builder.RegisterType<SurnameRepository>()
            .As<ISurnameRepository>();

        builder.RegisterType<TeacherFullNameRepository>()
            .As<ITeacherFullNameRepository>();
        
        builder.RegisterType<TeacherRepository>()
            .As<ITeacherRepository>();

        builder.RegisterType<TimetableRepository>()
            .As<ITimetableRepository>();
    }
}