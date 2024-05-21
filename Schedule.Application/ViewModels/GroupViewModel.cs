using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class GroupViewModel : IMapWith<Group>
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public void Map(Profile profile)
    {
        profile.CreateMap<Group, GroupViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(group => group.GroupId));

        profile.CreateMap<GroupViewModel, Group>()
            .ForMember(group => group.GroupId, expression =>
                expression.MapFrom(viewModel => viewModel.Id));
    }
}