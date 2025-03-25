using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using JetBrains.Annotations;
using TourPlanner.Models.Interfaces;
using TourPlanner.Models.Models;
using TourPlanner.Presentation.ViewModels.Commands;

namespace TourPlanner.Presentation.ViewModels;

public sealed class AddTourLogViewModel : INotifyPropertyChanged
{
    private readonly ITourLogService _tourLogService;
    private readonly Tour _tour;
    
    private DateTime _dateTime = DateTime.Now;
    private string _comment = string.Empty;
    private int _difficulty = 3;
    private double _totalDistance;
    private TimeSpan _totalTime = TimeSpan.Zero;
    private int _rating = 3;

    public DateTime DateTime
    {
        get => _dateTime;
        set
        {
            _dateTime = value;
            OnPropertyChanged();
        }
    }

    public string Comment
    {
        get => _comment;
        set
        {
            _comment = value;
            OnPropertyChanged();
            ValidateFields();
        }
    }

    public int Difficulty
    {
        get => _difficulty;
        set
        {
            _difficulty = value;
            OnPropertyChanged();
        }
    }

    public double TotalDistance
    {
        get => _totalDistance;
        set
        {
            _totalDistance = value;
            OnPropertyChanged();
            ValidateFields();
        }
    }
    
    public string TotalTimeString
    {
        get => _totalTime.TotalHours.ToString("0.0");
        set
        {
            if (double.TryParse(value, out double hours))
            {
                _totalTime = TimeSpan.FromHours(hours);
                OnPropertyChanged();
            }
        }
    }

    public int Rating
    {
        get => _rating;
        set
        {
            _rating = value;
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
            (AddTourLogCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }
    }

    [UsedImplicitly]
    public ICommand AddTourLogCommand { get; }
    
    [UsedImplicitly]
    public ICommand CancelCommand { get; }

    public AddTourLogViewModel(ITourLogService tourLogService, Tour tour)
    {
        _tourLogService = tourLogService;
        _tour = tour;
        
        _totalDistance = tour.Distance;
        OnPropertyChanged(nameof(TotalDistance));

        AddTourLogCommand = new RelayCommand(ExecuteAddTourLog, CanExecuteAddTourLog);
        CancelCommand = new RelayCommand(ExecuteCancel);
        ValidateFields();
    }

    private bool CanExecuteAddTourLog(object? parameter) => IsValid;

    private void ExecuteAddTourLog(object? parameter)
    {
        var tourLog = new TourLog
        {
            TourId = _tour.Id,
            DateTime = DateTime,
            Comment = Comment,
            Difficulty = Difficulty,
            TotalDistance = TotalDistance,
            TotalTime = _totalTime,
            Rating = Rating
        };

        if (_tourLogService.AddTourLog(tourLog))
        {
            _tour.TourLogs.Add(tourLog);
            
            if (parameter is Window window)
            {
                window.DialogResult = true;
                window.Close();
            }
        }
    }

    private void ExecuteCancel(object? parameter)
    {
        if (parameter is Window window)
        {
            window.DialogResult = false;
            window.Close();
        }
    }

    private void ValidateFields()
    {
        IsValid = !string.IsNullOrWhiteSpace(Comment) &&
                  TotalDistance >= 0 &&
                  _totalTime.TotalMinutes >= 0;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}