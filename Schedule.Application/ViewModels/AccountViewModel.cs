using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class AccountViewModel : IMapWith<Account>
{
    public int Id { get; set; }
    
    public string Login { get; set; } = null!;
    
    public string PasswordHash { get; set; } = null!;
    
    public NameViewModel Name { get; set; } = null!;
    
    public SurnameViewModel Surname { get; set; } = null!;
    
    public MiddleNameViewModel? MiddleName { get; set; }
    
    public string Email { get; set; } = null!;
    
    public RoleViewModel Role { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<Account, AccountViewModel>()
            .ForMember(account => account.Id, expression =>
                expression.MapFrom(account => account.AccountId))
            .ForMember(viewModel => viewModel.Name, expression =>
                expression.MapFrom(account => new NameViewModel
                {
                    Value = account.Name
                }))
            .ForMember(viewModel => viewModel.Surname, expression =>
                expression.MapFrom(account => new SurnameViewModel
                {
                    Value = account.Surname
                }))
            .ForMember(viewModel => viewModel.MiddleName, expression =>
                expression.MapFrom(account => new MiddleNameViewModel
                {
                    Value = account.MiddleName
                }));

        profile.CreateMap<AccountViewModel, Account>()
            .ForMember(viewModel => viewModel.AccountId, expression =>
                expression.MapFrom(viewModel => viewModel.Id));
    }
}