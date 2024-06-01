using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class MiddleNameViewModel : IMapWith<MiddleName>
{
    public string Value { get; set; } = null!;

    public void Map(Profile profile)
    {
        profile.CreateMap<string, MiddleNameViewModel>()
            .ForMember(viewModel => viewModel.Value, expression =>
                expression.MapFrom(e => e));
        
        profile.CreateMap<MiddleName, MiddleNameViewModel>()
            .ForMember(viewModel => viewModel.Value, expression =>
                expression.MapFrom(middleName => middleName.Value));
    }
}