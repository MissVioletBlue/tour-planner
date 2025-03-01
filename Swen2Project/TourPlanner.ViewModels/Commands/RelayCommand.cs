using System.Windows.Input;

namespace Swen2Project.TourPlanner.ViewModels.Commands;

public class RelayCommand : ICommand
{
    public RelayCommand(Action<object> execute, Predicate<object> canExecute)
    {
        ExecuteCommand = execute;
        CanExecuteCommand = canExecute;
    }
    
    private Action<object?> ExecuteCommand { get; set; }
    private Predicate<object?> CanExecuteCommand { get; set; }
    
    public bool CanExecute(object? parameter)
    {
        return CanExecuteCommand(parameter);
    }

    public void Execute(object? parameter)
    {
        ExecuteCommand(parameter);
    }

    public event EventHandler? CanExecuteChanged;
}