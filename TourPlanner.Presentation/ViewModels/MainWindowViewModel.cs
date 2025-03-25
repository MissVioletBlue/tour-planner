using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using TourPlanner.Models.Interfaces;
using TourPlanner.Models.Models;
using TourPlanner.Presentation.Presentation.Views;
using TourPlanner.Presentation.ViewModels.Commands;
using JetBrains.Annotations;

namespace TourPlanner.Presentation.ViewModels;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private readonly ITourService _tourService;
    private Tour? _selectedTour;

    public ObservableCollection<Tour> Tours => _tourService.Tours;

    public Tour? SelectedTour
    {
        get => _selectedTour;
        set
        {
            _selectedTour = value;
            OnPropertyChanged();
            (ShowRemoveTourCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }
    }
    
    [UsedImplicitly]
    public ICommand ShowAddTourCommand { get; }
    public ICommand ShowRemoveTourCommand { get; }

    public MainWindowViewModel(ITourService tourService)
    {
        _tourService = tourService;
        
        ShowAddTourCommand = new RelayCommand(ShowAddTourWindow);
        ShowRemoveTourCommand = new RelayCommand(ShowRemoveTourWindow, CanShowRemoveTourWindow);
        
        if (!Tours.Any())
        {
            AddSampleTours();
        }
    }

    private void AddSampleTours()
    {
        var tour1 = new Tour
        {
            Name = "Vienna to Salzburg",
            StartLocation = "Vienna",
            DestinationLocation = "Salzburg",
            TransportType = "Bicycle",
            Distance = 295,
            EstimatedTime = TimeSpan.FromHours(14),
            ChildFriendliness = 2,
            Popularity = 4,
            Description = "A scenic route from Vienna to Salzburg along the Danube.",
            RouteType = "Scenic",
            SurfaceType = "Paved",
            DifficultyLevel = "Moderate"
        };

        var tour2 = new Tour
        {
            Name = "Innsbruck Mountain Trail",
            StartLocation = "Innsbruck",
            DestinationLocation = "Hafelekar Peak",
            TransportType = "Hiking",
            Distance = 8,
            EstimatedTime = TimeSpan.FromHours(3.5),
            ChildFriendliness = 3,
            Popularity = 5,
            Description = "A beautiful mountain hike with panoramic views of Innsbruck.",
            RouteType = "Mountain",
            SurfaceType = "Rocky",
            DifficultyLevel = "Hard"
        };

        _tourService.AddTour(tour1);
        _tourService.AddTour(tour2);
    }

    private void ShowAddTourWindow(object? parameter)
    {
        var viewModel = new AddTourViewModel(_tourService);
        var addTourWindow = new AddingTourWindow
        {
            DataContext = viewModel,
            Owner = Application.Current.MainWindow
        };
        
        addTourWindow.ShowDialog();
    }

    private void ShowRemoveTourWindow(object? parameter)
    {
        if (SelectedTour == null) return;
        
        var removeTourWindow = new RemovingTourWindow
        {
            DataContext = new RemoveTourViewModel(_tourService, SelectedTour),
            Owner = Application.Current.MainWindow
        };
        
        removeTourWindow.ShowDialog();
    }

    private bool CanShowRemoveTourWindow(object? parameter)
    {
        return SelectedTour != null;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}