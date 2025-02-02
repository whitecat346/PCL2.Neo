using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using PCL2.Neo.Helpers;

namespace PCL2.Neo.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        navBackgroundBorder.PointerPressed += OnNavPointerPressed;
        new ThemeHelper(this).Refresh();

        BtnTitleClose.Click += (_, _) => Close();
        BtnTitleMin.Click += (_, _) => WindowState = WindowState.Minimized;
    }

    private void OnNavPointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e) {
        this.BeginMoveDrag(e);
    }
}