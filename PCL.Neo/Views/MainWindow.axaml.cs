using Avalonia.Controls;
using Avalonia.Input;
using PCL.Neo.ViewModels;

namespace PCL.Neo.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        PointerPressed += OnPointerPressed;
    }

    private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        var props = e.GetCurrentPoint(this).Properties;

        if (DataContext is not MainWindowViewModel vm) return;

        if (props.IsXButton1Pressed)
        {
            if (vm.GoBackCommand.CanExecute(null))
            {
                vm.GoBackCommand.Execute(null);
                e.Handled = true;
            }
        }

        if (props.IsXButton2Pressed)
        {
            if (vm.GoForwardCommand.CanExecute(null))
            {
                vm.GoForwardCommand.Execute(null);
                e.Handled = true;
            }
        }
    }
}
