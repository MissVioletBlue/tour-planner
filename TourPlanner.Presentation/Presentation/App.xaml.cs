﻿using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using TourPlanner.Business.Services;
using TourPlanner.DataAccess.Repositories;
using TourPlanner.Models.Interfaces;
using TourPlanner.Presentation.Presentation.Views;
using TourPlanner.Presentation.ViewModels;

namespace TourPlanner.Presentation;

public partial class App : Application
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
        
        // Register services
        services.AddSingleton<ITourService, TourService>();
        
        // Register view models
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<AddTourViewModel>();
        
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