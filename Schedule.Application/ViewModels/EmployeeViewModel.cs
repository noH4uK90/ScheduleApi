using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class EmployeeViewModel : IMapWith<Employee>
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? MiddleName { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<Employee, EmployeeViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(employee => employee.EmployeeId))
            .ForMember(viewModel => viewModel.Login, expression =>
                expression.MapFrom(employee => employee.Account.Login))
            .ForMember(viewModel => viewModel.Email, expression =>
                expression.MapFrom(employee => employee.Account.Email))
            .ForMember(viewModel => viewModel.Name, expression =>
                expression.MapFrom(employee => employee.Account.Name))
            .ForMember(viewModel => viewModel.Surname, expression =>
                expression.MapFrom(employee => employee.Account.Surname))
            .ForMember(viewModel => viewModel.MiddleName, expression =>
                expression.MapFrom(employee => employee.Account.MiddleName))
            .ReverseMap();

        profile.CreateMap<EmployeeViewModel, Employee>()
            .ForMember(employee => employee.EmployeeId, expression =>
                expression.MapFrom(viewModel => viewModel.Id))
            .ForMember(employee => employee.Account, expression =>
                expression.MapFrom(viewModel => viewModel.Login))
            .ForPath(employee => employee.Account.Login, expression =>
                expression.MapFrom(viewModel => viewModel.Login))
            .ForMember(employee => employee.Account, expression =>
                expression.MapFrom(viewModel => viewModel.Email))
            .ForPath(employee => employee.Account.Email, expression =>
                expression.MapFrom(viewModel => viewModel.Email))
            .ForMember(employee => employee.Account, expression =>
                expression.MapFrom(viewModel => viewModel.Name))
            .ForPath(employee => employee.Account.Name, expression =>
                expression.MapFrom(viewModel => viewModel.Name))
            .ForMember(employee => employee.Account, expression =>
                expression.MapFrom(viewModel => viewModel.Surname))
            .ForPath(employee => employee.Account.Surname, expression =>
                expression.MapFrom(viewModel => viewModel.Surname))
            .ForMember(employee => employee.Account, expression =>
                expression.MapFrom(viewModel => viewModel.MiddleName))
            .ForPath(employee => employee.Account.MiddleName, expression =>
                expression.MapFrom(viewModel => viewModel.MiddleName))
            .ReverseMap();
    }
}