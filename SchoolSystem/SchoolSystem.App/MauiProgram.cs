using System.Reflection;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using DAL.Migrator;
using DAL.Options;
using Microsoft.Extensions.Configuration;
using SchoolSystem.App.Services.Interfaces;
using SchoolSystem.BL;
using SchoolSystem.DAL;
using DAL.Migrator;
using DAL.Options;

namespace SchoolSystem.App;

public static class MauiProgram {
    public static MauiApp CreateMauiApp() {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts => {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .ConfigureAppSettings();
        
        builder.Services
            .AddDALServices(builder.Configuration.GetDALOptions())
            .AddBLServices()
            .AddAppServices();
        
#if DEBUG
        builder.Logging.AddDebug();
#endif
        
        var app = builder.Build();
        
        app.Services.GetRequiredService<IDbMigrator>().Migrate();
        RegisterRouting(app.Services.GetRequiredService<INavigationService>());
        
        return builder.Build();
    }
    
    private static void RegisterRouting(INavigationService navigationService) {
        foreach (var route in navigationService.Routes) {
            Routing.RegisterRoute(route.Route, route.ViewType);
        }
    }
    
    private static void ConfigureAppSettings(this MauiAppBuilder builder) {
        var configBuilder = new ConfigurationBuilder();
        const string appSettingsPath = "SchoolSystem.App.appsettings.json";
        using var appSettingsStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(appSettingsPath);
        if (appSettingsStream is not null) {
            configBuilder.AddJsonStream(appSettingsStream);
        }
        
        builder.Configuration.AddConfiguration(configBuilder.Build());
    }
    
    private static DALOptions GetDALOptions(this IConfiguration config) {
        var opts = new DALOptions {
            DatabaseDirectory = FileSystem.AppDataDirectory
        };
        config.GetSection("SchoolSystem:DAL").Bind(opts);
        return opts;
    }
}