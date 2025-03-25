using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using TourPlanner.Business.Services;
using TourPlanner.DataAccess.Repositories;
using TourPlanner.Models.Interfaces;
using TourPlanner.Presentation.Presentation.Views;
using TourPlanner.Presentation.ViewModels;

namespace TourPlanner.Presentation.Presentation;

public partial class App
{
    private ServiceProvider _serviceProvider;

    public App()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        _serviceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(ServiceCollection services)
    {
        // Register repositories
        services.AddSingleton<ITourRepository, TourRepository>();
        services.AddSingleton<ITourLogRepository, TourLogRepository>();
    
        // Register services
        services.AddSingleton<ITourService, TourService>();
        services.AddSingleton<ITourLogService, TourLogService>();
    
        // Register view models
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<AddTourViewModel>();
        services.AddTransient<RemoveTourViewModel>();
        services.AddTransient<AddTourLogViewModel>();
    
        // Register views
        services.AddTransient<MainWindow>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        var mainWindow = _serviceProvider.GetService<MainWindow>();
        mainWindow!.DataContext = _serviceProvider.GetService<MainWindowViewModel>();
        mainWindow.Show();
    }
}