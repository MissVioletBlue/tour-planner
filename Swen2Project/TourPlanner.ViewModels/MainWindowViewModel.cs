using System.ComponentModel;
using System.Windows.Input;
using Swen2Project.TourPlanner.Presentation.Views;
using Swen2Project.TourPlanner.ViewModels.Commands;

namespace Swen2Project.TourPlanner.ViewModels;

public class MainWindowViewModel
{
    public MainWindowViewModel()
    {
        ShowAddTourCommand = new RelayCommand(ShowAddTourWindow, CanShowAddTourWindow);
    }
    
    public ICommand ShowAddTourCommand { set; get; }
    
    private static void ShowAddTourWindow(object? obj)
    {
        var addTourWindow = new AddingTourWindow();
        addTourWindow.Show();
    }
    
    private static bool CanShowAddTourWindow(object obj)
    {
        return true;
    }
}