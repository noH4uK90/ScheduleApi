using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class RoleViewModel : IMapWith<Role>
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public void Map(Profile profile)
    {
        profile.CreateMap<Role, RoleViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(role => role.RoleId))
            .ReverseMap();
    }
}