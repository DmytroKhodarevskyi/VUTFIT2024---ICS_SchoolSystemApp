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

        //ConfigureAppSettings(builder);

        builder.Services
            .AddDALServices(GetDALOptions(builder.Configuration))
            .AddAppServices()
            .AddBLServices();
        

        var app = builder.Build();

        MigrateDb(app.Services.GetRequiredService<IDbMigrator>());
        RegisterRouting(app.Services.GetRequiredService<INavigationService>());

        return app;
    }

    private static void ConfigureAppSettings(MauiAppBuilder builder)
    {
        var configurationBuilder = new ConfigurationBuilder()
            // .SetBasePath(Directory.GetCurrentDirectory())
            // .AddJsonFile("appsettings.json")
            .Build();
        //
        // var assembly = Assembly.GetExecutingAssembly();
        // const string appSettingsFilePath = "SchoolSystem.App.appsettings.json";
        // using var appSettingsStream = assembly.GetManifestResourceStream(appSettingsFilePath);
        // if (appSettingsStream is not null)
        // {
        //     configurationBuilder.AddJsonStream(appSettingsStream);
        // }
        //
        builder.Configuration.AddConfiguration(configurationBuilder);
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
            RecreateDatabaseEachTime = true,
            SeedDemoData = true
            
        };
        
       // configuration.GetSection("SchoolSystem:DAL").Bind(dalOptions);
        return dalOptions;
    }

    private static void MigrateDb(IDbMigrator migrator) => migrator.Migrate();
}