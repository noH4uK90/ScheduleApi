using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Name = Schedule.Core.Models.Name;

namespace Schedule.Application.ViewModels;

public class NameViewModel : IMapWith<Name>
{
    public string Value { get; set; } = null!;

    public void Map(Profile profile)
    {
        profile.CreateMap<Name, NameViewModel>()
            .ForMember(viewModel => viewModel.Value, expression =>
                expression.MapFrom(name => name.Value));
    }
}