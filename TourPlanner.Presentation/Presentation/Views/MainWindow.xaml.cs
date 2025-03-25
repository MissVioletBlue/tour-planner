using System.Windows;
using TourPlanner.Presentation.ViewModels;

namespace TourPlanner.Presentation.Presentation.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        var mainWindowViewModel = new MainWindowViewModel();
        DataContext = mainWindowViewModel;
    }
}