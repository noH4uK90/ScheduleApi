using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class SurnameViewModel : IMapWith<Surname>
{
    public string Value { get; set; } = null!;

    public void Map(Profile profile)
    {
        profile.CreateMap<Surname, SurnameViewModel>()
            .ForMember(viewModel => viewModel.Value, expression =>
                expression.MapFrom(surname => surname.Value));
    }
}