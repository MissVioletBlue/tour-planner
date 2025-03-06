using System.Windows.Input;
using Swen2Project.TourPlanner.Presentation.Views;
using Swen2Project.TourPlanner.ViewModels.Commands;

namespace Swen2Project.TourPlanner.ViewModels;

public class MainWindowViewModel
{
    public ICommand ShowAddTourCommand { get; } = new RelayCommand(ShowAddTourWindow, CanShowAddTourWindow);
    public ICommand ShowRemoveTourCommand { get; } = new RelayCommand(ShowRemoveTourWindow, CanShowRemoveTourWindow);

    private static void ShowAddTourWindow(object? obj)
    {
        var addTourWindow = new AddingTourWindow();
        addTourWindow.ShowDialog();
    }
    
    private static bool CanShowAddTourWindow(object? obj)
    {
        return true;
    }
    
    private static void ShowRemoveTourWindow(object? obj)
    {
        var removeTourWindow = new RemovingTourWindow();
        removeTourWindow.ShowDialog();
    }
    
    private static bool CanShowRemoveTourWindow(object? obj)
    {
        return true;
    }
}