using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using TourPlanner.Models;
using TourPlanner.Models.Interfaces;
using TourPlanner.Models.Models;
using TourPlanner.Presentation.ViewModels.Commands;

namespace TourPlanner.Presentation.ViewModels;

public class RemoveTourViewModel : INotifyPropertyChanged
{
    private readonly ITourService _tourService;
    private readonly Tour _tourToRemove;

    public Tour TourToRemove => _tourToRemove;

    public ICommand RemoveTourCommand { get; }
    public ICommand CancelCommand { get; }

    public RemoveTourViewModel(ITourService tourService, Tour tourToRemove)
    {
        _tourService = tourService;
        _tourToRemove = tourToRemove;
        
        RemoveTourCommand = new RelayCommand(ExecuteRemoveTour);
        CancelCommand = new RelayCommand(ExecuteCancel);
    }

    private void ExecuteRemoveTour(object? parameter)
    {
        _tourService.DeleteTour(_tourToRemove.Id);
        
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

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}