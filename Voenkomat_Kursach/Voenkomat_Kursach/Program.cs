using Avalonia;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Voenkomat_Kursach.Models;
using Voenkomat_Kursach.ViewModels;
using Voenkomat_Kursach.Views;

namespace Voenkomat_Kursach;

sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {

        var host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json").AddEnvironmentVariables();
            })
            .ConfigureServices((context, services) =>
            {

                services.Configure<AppSettings>(context.Configuration.GetSection("AppSettings"));

                services.AddTransient<MainWindow>();
                services.AddTransient<MainWindowViewModel>();

                services.AddTransient<DoctorWindow>();
                services.AddTransient<DoctorViewModel>();

            }).Build();
        
        BuildAvaloniaApp(host.Services)
            .StartWithClassicDesktopLifetime(args);
        
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp(IServiceProvider provider)
        => AppBuilder.Configure(() => new App(provider))
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}