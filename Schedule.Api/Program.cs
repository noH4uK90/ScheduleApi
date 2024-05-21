using System.Text.Json.Serialization;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Schedule.Api.Middleware.CustomException;
using Schedule.Application.Modules;
using Schedule.Common;
using Schedule.Modules;

var applicationBuilder = WebApplication.CreateBuilder(args);

applicationBuilder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory(builder =>
    {
        var configuration = applicationBuilder.Configuration;
            
        builder.RegisterModule(new ApiModule(configuration));
        builder.RegisterModule<ApplicationModule>();
    }))
    .ConfigureServices(services =>
    {
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
    });

var app = applicationBuilder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

ConfigureApp(app);

app.Run();

void ConfigureApp(WebApplication webApp)
{
    webApp.UseRequestLocalization();
    webApp
        .UseCustomExceptionHandler()
        .UseSwagger()
        .UseSwaggerUI();
    webApp.UseRouting();
    webApp.UseCors(Constants.CorsName);
    webApp.UseAuthentication();
    webApp.UseAuthorization();
    webApp.MapControllers();
}