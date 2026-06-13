using Avalonia;
using System;
using System.IO;
using System.Text.Json;
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

        try
        {
            using (var fs = File.OpenRead("appsettings.json"))
            {
                JsonSerializer.Deserialize<AppSettings>(fs);
            }
        }
        catch (Exception e)//если файла нет или он плохой
        {
            Console.WriteLine(e);
            MakeMtFile();
            Console.WriteLine('\a');
        }

        var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {

                services.AddSingleton<AppSettings>();

                services.AddTransient<MainWindow>();
                services.AddTransient<MainWindowViewModel>();

                services.AddTransient<AdminWIndow>();
                services.AddTransient<AdminViewModel>();

                services.AddTransient<ArchiverWindow>();
                services.AddTransient<ArchiverViewModel>();


                services.AddTransient<RegistratorWindow>();
                services.AddTransient<RegistatorViewModel>();


                services.AddTransient<DoctorWindow>();
                services.AddTransient<DoctorViewModel>();

                
                services.AddTransient<ComissionWindow>();
                services.AddTransient<ComissionViewModel>();

            }).Build();
        
        BuildAvaloniaApp(host.Services)
            .StartWithClassicDesktopLifetime(args);
        
    }

    private static void MakeMtFile()
    {
        using (var fs = File.Create("appsettings.json"))
        {
            JsonSerializer.Serialize(fs, new AppSettings());
        }
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp(IServiceProvider provider)
        => AppBuilder.Configure(() => new App(provider))
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}


