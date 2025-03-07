using System.Windows.Input;
using Swen2Project.TourPlanner.Presentation.Views;
using Swen2Project.TourPlanner.ViewModels.Commands;

namespace Swen2Project.TourPlanner.ViewModels;

public class MainWindowViewModel
{
    public MainWindowViewModel()
    {
        ShowAddTourCommand = new RelayCommand(ShowAddTourWindow, CanShowAddTourWindow);
        ShowRemoveTourCommand = new RelayCommand(ShowRemoveTourWindow, CanShowRemoveTourWindow);
    }

    public ICommand ShowAddTourCommand { get; }
    public ICommand ShowRemoveTourCommand { get; }

    private void ShowAddTourWindow(object? obj)
    {
        var addTourWindow = new AddingTourWindow();
        addTourWindow.ShowDialog();
    }
    
    private bool CanShowAddTourWindow(object? obj)
    {
        return true;
    }
    
    private void ShowRemoveTourWindow(object? obj)
    {
        var removeTourWindow = new RemovingTourWindow();
        removeTourWindow.ShowDialog();
    }
    
    private bool CanShowRemoveTourWindow(object? obj)
    {
        return true;
    }
}