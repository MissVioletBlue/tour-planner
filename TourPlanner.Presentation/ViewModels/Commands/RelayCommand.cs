using System.Windows.Input;

namespace TourPlanner.Presentation.ViewModels.Commands;

public class RelayCommand : ICommand
{
    private readonly Action<object?> _executeCommand;
    private readonly Func<object?, bool>? _canExecuteCommand;
    
    public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
    {
        _executeCommand = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecuteCommand = canExecute;
    }
    
    public RelayCommand(Action<object?> execute) : this(execute, null)
    {
    }
    
    public bool CanExecute(object? parameter)
    {
        return _canExecuteCommand?.Invoke(parameter) ?? true;
    }

    public void Execute(object? parameter)
    {
        _executeCommand(parameter);
    }

    public event EventHandler? CanExecuteChanged;
    
    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}