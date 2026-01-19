using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using PCL.Neo.ViewModels;
using PageViewModelBase = PCL.Neo.ViewModels.Pages.PageViewModelBase;

namespace PCL.Neo;

public partial class HomeView : PageViewModelBase
{
    public HomeView()
    {
        InitializeComponent();
    }
}
