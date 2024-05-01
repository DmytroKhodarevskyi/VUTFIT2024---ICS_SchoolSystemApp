using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using SchoolSystem.BL;
using DAL.Migrator;
using DAL.Options;
using DAL;
using SchoolSystem.App.Services;
using SchoolSystem.App.Services.Interfaces;
using SchoolSystem.DAL;

[assembly:System.Resources.NeutralResourcesLanguage("en")]
namespace SchoolSystem.App;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });


        builder.Services
            .AddDALServices(GetDALOptions(builder.Configuration))
            .AddAppServices()
            .AddBLServices();
        

        var app = builder.Build();

        MigrateDb(app.Services.GetRequiredService<IDbMigrator>());
        RegisterRouting(app.Services.GetRequiredService<INavigationService>());

        return app;
    }


    private static void RegisterRouting(INavigationService navigationService)
    {
        foreach (var route in navigationService.Routes)
        {
            Routing.RegisterRoute(route.Route, route.ViewType);
        }
    }

    private static DALOptions GetDALOptions(IConfiguration configuration)
    {
        // Extract values
        DALOptions dalOptions = new()
        {
            DatabaseDirectory = FileSystem.AppDataDirectory,
            DatabaseName = "schoolsystem.db",
            RecreateDatabaseEachTime = false,
            SeedDemoData = false
            
        };
        
        return dalOptions;
    }

    private static void MigrateDb(IDbMigrator migrator) => migrator.Migrate();
}