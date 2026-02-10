using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using SharpCogsInc.Factories;
using SharpCogsInc.Models;
using SharpCogsInc.Services;
using SharpCogsInc.ViewModels;
using SharpCogsInc.Views;

namespace SharpCogsInc;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var collection = new ServiceCollection();
        collection.AddSingleton<MainViewModel>();
        collection.AddSingleton<MainView>();
        collection.AddSingleton<PageFactory>();
        collection.AddSingleton<INavigationService, NavigationService>();
        collection.AddSingleton<IFolderPickerService>(provider =>
        {
            var mainWindow = provider.GetRequiredService<MainView>();
            return new AvaloniaFolderPickerService(() => mainWindow);
        });
        collection.AddTransient<RegisterViewModel>();
        collection.AddTransient<HomeViewModel>();
        collection.AddTransient<SettingsViewModel>();
        collection.AddHttpClient<ApiService>(client =>
        {
            client.DefaultRequestHeaders.UserAgent.ParseAdd("SharpCogsInc/1.0.0");
            client.BaseAddress = new Uri("https://corporateclash.net/api/v1/");
        });
        collection.AddSingleton<Func<ApplicationPageNames, PageViewModel>>(x => name => name switch
        {
            ApplicationPageNames.Home => x.GetRequiredService<HomeViewModel>(),
            ApplicationPageNames.Register => x.GetRequiredService<RegisterViewModel>(),
            ApplicationPageNames.Settings => x.GetRequiredService<SettingsViewModel>(),
            _ => throw new InvalidOperationException()
        });


        collection.AddSingleton<LinuxSettings>(_ => FileService.LoadSettings() ?? new LinuxSettings());
        var services = collection.BuildServiceProvider();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow = services.GetRequiredService<MainView>();
            desktop.MainWindow.DataContext = services.GetRequiredService<MainViewModel>();

        }

        base.OnFrameworkInitializationCompleted();
        
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}