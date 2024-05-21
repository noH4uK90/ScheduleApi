using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class StudentViewModel : IMapWith<Student>
{
    public int Id { get; set; }
    
    public GroupViewModel Group { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? MiddleName { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<Student, StudentViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(student => student.StudentId))
            .ForMember(viewModel => viewModel.Login, expression =>
                expression.MapFrom(student => student.Account.Login))
            .ForMember(viewModel => viewModel.Email, expression =>
                expression.MapFrom(student => student.Account.Email))
            .ForMember(viewModel => viewModel.Name, expression =>
                expression.MapFrom(student => student.Account.Name))
            .ForMember(viewModel => viewModel.Surname, expression =>
                expression.MapFrom(student => student.Account.Surname))
            .ForMember(viewModel => viewModel.MiddleName, expression =>
                expression.MapFrom(student => student.Account.MiddleName));

        profile.CreateMap<StudentViewModel, Student>()
            .ForMember(student => student.StudentId, expression =>
                expression.MapFrom(viewModel => viewModel.Id))
            .ForMember(student => student.Account, expression =>
                expression.MapFrom(viewModel => viewModel.Login))
                .ForPath(student => student.Account.Login, expression =>
                    expression.MapFrom(viewModel => viewModel.Login))
            .ForMember(student => student.Account, expression =>
                expression.MapFrom(viewModel => viewModel.Email))
                .ForPath(student => student.Account.Email, expression =>
                    expression.MapFrom(viewModel => viewModel.Email))
            .ForMember(student => student.Account, expression =>
                expression.MapFrom(viewModel => viewModel.Name))
                .ForPath(student => student.Account.Name, expression =>
                    expression.MapFrom(viewModel => viewModel.Name))
            .ForMember(student => student.Account, expression =>
                expression.MapFrom(viewModel => viewModel.Surname))
                .ForPath(student => student.Account.Surname, expression =>
                    expression.MapFrom(viewModel => viewModel.Surname))
            .ForMember(student => student.Account, expression =>
                expression.MapFrom(viewModel => viewModel.MiddleName))
                .ForPath(student => student.Account.MiddleName, expression =>
                    expression.MapFrom(viewModel => viewModel.MiddleName));
    }
}