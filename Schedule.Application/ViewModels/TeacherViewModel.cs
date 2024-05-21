using AutoMapper;
using Schedule.Core.Common.Extensions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class TeacherViewModel : IMapWith<Teacher>
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? MiddleName { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<Teacher, TeacherViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(teacher => teacher.TeacherId))
            .ForMember(viewModel => viewModel.Login, expression =>
                expression.MapFrom(teacher => teacher.Account.Login))
            .ForMember(viewModel => viewModel.Email, expression =>
                expression.MapFrom(teacher => teacher.Account.Email))
            .ForMember(viewModel => viewModel.Name, expression =>
                expression.MapFrom(teacher => teacher.Account.Name))
            .ForMember(viewModel => viewModel.Surname, expression =>
                expression.MapFrom(teacher => teacher.Account.Surname))
            .ForMember(viewModel => viewModel.MiddleName, expression =>
                expression.MapFrom(teacher => teacher.Account.MiddleName));

        profile.CreateMap<TeacherViewModel, Teacher>()
            .ForMember(teacher => teacher.TeacherId, expression =>
                expression.MapFrom(viewModel => viewModel.Id))
            .ForMember(teacher => teacher.Account, expression =>
                expression.MapFrom(viewModel => viewModel.Login))
                .ForPath(teacher => teacher.Account.Login, expression =>
                    expression.MapFrom(viewModel => viewModel.Login))
            .ForMember(teacher => teacher.Account, expression =>
                expression.MapFrom(viewModel => viewModel.Email))
                .ForPath(teacher => teacher.Account.Email, expression =>
                    expression.MapFrom(viewModel => viewModel.Email))
            .ForMember(teacher => teacher.Account, expression =>
                expression.MapFrom(viewModel => viewModel.Name))
                .ForPath(teacher => teacher.Account.Name, expression =>
                    expression.MapFrom(viewModel => viewModel.Name))
            .ForMember(teacher => teacher.Account, expression =>
                expression.MapFrom(viewModel => viewModel.Surname))
                .ForPath(teacher => teacher.Account.Surname, expression =>
                    expression.MapFrom(viewModel => viewModel.Surname))
            .ForMember(teacher => teacher.Account, expression =>
                expression.MapFrom(viewModel => viewModel.MiddleName))
                .ForPath(teacher => teacher.Account.MiddleName, expression =>
                    expression.MapFrom(viewModel => viewModel.MiddleName));
    }
}