namespace Schedule.Application.ViewModels;

public sealed class AuthorizationResultViewModel
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
    public required AccountViewModel Account { get; set; }
}