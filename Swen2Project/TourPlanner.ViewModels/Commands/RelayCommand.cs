﻿using System.Windows.Input;

namespace Swen2Project.TourPlanner.ViewModels.Commands;

public class RelayCommand : ICommand
{
    private readonly Action<object?> _executeCommand;
    private readonly Func<object?, bool>? _canExecuteCommand;
    
    public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute)
    {
        _executeCommand = execute;
        _canExecuteCommand = canExecute;
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
}