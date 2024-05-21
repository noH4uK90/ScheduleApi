using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class DisciplineViewModel : IMapWith<Discipline>
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public void Map(Profile profile)
    {
        profile.CreateMap<Discipline, DisciplineViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(discipline => discipline.DisciplineId))
            .ReverseMap();
    }
}