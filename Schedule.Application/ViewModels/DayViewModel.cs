using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class DayViewModel : IMapWith<Day>
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsStudy { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<Day, DayViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(day => day.DayId))
            .ReverseMap();
    }
}