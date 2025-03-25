using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using TourPlanner.Models.Interfaces;
using TourPlanner.Models.Models;
using TourPlanner.Presentation.ViewModels.Commands;

namespace TourPlanner.Presentation.ViewModels;

public class AddTourViewModel : INotifyPropertyChanged
{
    private readonly ITourService _tourService;
    private string _name = string.Empty;
    private string _startLocation = string.Empty;
    private string _destinationLocation = string.Empty;
    private string _transportType = "Bicycle";
    private double _distance;
    private TimeSpan _estimatedTime = TimeSpan.Zero;
    private int _childFriendliness = 3;
    private int _popularity = 3;
    private string _description = string.Empty;
    private string _routeType = string.Empty;
    private string _surfaceType = string.Empty;
    private string _difficultyLevel = "Moderate";

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged();
            ValidateFields();
        }
    }

    public string StartLocation
    {
        get => _startLocation;
        set
        {
            _startLocation = value;
            OnPropertyChanged();
            ValidateFields();
        }
    }

    public string DestinationLocation
    {
        get => _destinationLocation;
        set
        {
            _destinationLocation = value;
            OnPropertyChanged();
            ValidateFields();
        }
    }

    public string TransportType
    {
        get => _transportType;
        set
        {
            _transportType = value;
            OnPropertyChanged();
        }
    }

    public double Distance
    {
        get => _distance;
        set
        {
            _distance = value;
            OnPropertyChanged();
        }
    }

    public string EstimatedTimeString
    {
        get => _estimatedTime.TotalHours.ToString("0.0");
        set
        {
            if (double.TryParse(value, out double hours))
            {
                _estimatedTime = TimeSpan.FromHours(hours);
                OnPropertyChanged();
            }
        }
    }

    public int ChildFriendliness
    {
        get => _childFriendliness;
        set
        {
            _childFriendliness = value;
            OnPropertyChanged();
        }
    }

    public int Popularity
    {
        get => _popularity;
        set
        {
            _popularity = value;
            OnPropertyChanged();
        }
    }

    public string Description
    {
        get => _description;
        set
        {
            _description = value;
            OnPropertyChanged();
        }
    }

    public string RouteType
    {
        get => _routeType;
        set
        {
            _routeType = value;
            OnPropertyChanged();
        }
    }

    public string SurfaceType
    {
        get => _surfaceType;
        set
        {
            _surfaceType = value;
            OnPropertyChanged();
        }
    }

    public string DifficultyLevel
    {
        get => _difficultyLevel;
        set
        {
            _difficultyLevel = value;
            OnPropertyChanged();
        }
    }

    private bool _isValid;
    public bool IsValid
    {
        get => _isValid;
        set
        {
            _isValid = value;
            OnPropertyChanged();
            (AddTourCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }
    }

    public ICommand AddTourCommand { get; }
    public ICommand CancelCommand { get; }

    public AddTourViewModel(ITourService tourService)
    {
        _tourService = tourService;
        AddTourCommand = new RelayCommand(ExecuteAddTour, CanExecuteAddTour);
        CancelCommand = new RelayCommand(ExecuteCancel);
        ValidateFields();
    }

    private bool CanExecuteAddTour(object? parameter) => IsValid;

    private void ExecuteAddTour(object? parameter)
    {
        var tour = new Tour
        {
            Name = Name,
            StartLocation = StartLocation,
            DestinationLocation = DestinationLocation,
            TransportType = TransportType,
            Distance = Distance,
            EstimatedTime = _estimatedTime,
            ChildFriendliness = ChildFriendliness,
            Popularity = Popularity,
            Description = Description,
            RouteType = RouteType,
            SurfaceType = SurfaceType,
            DifficultyLevel = DifficultyLevel
        };

        _tourService.AddTour(tour);
        
        // Close the window
        if (parameter is Window window)
        {
            window.DialogResult = true;
            window.Close();
        }
    }

    private void ExecuteCancel(object? parameter)
    {
        // Close the window
        if (parameter is Window window)
        {
            window.DialogResult = false;
            window.Close();
        }
    }

    private void ValidateFields()
    {
        IsValid = !string.IsNullOrWhiteSpace(Name) &&
                 !string.IsNullOrWhiteSpace(StartLocation) &&
                 !string.IsNullOrWhiteSpace(DestinationLocation);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}