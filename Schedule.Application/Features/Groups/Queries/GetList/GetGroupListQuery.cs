using MediatR;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Queries.GetList;

public sealed record GetGroupListQuery : IRequest<ICollection<GroupViewModel>>
{
    public string? Search { get; set; }
}