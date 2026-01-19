using CommunityToolkit.Mvvm.ComponentModel;

namespace PCL.Neo.ViewModels.Pages;

public partial class PageViewModelBase(double defaultWidht = 250) : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase? _leftSidebar;

    [ObservableProperty]
    private ViewModelBase? _rightContent;

    [ObservableProperty]
    private double _sidebarWidth = defaultWidht;
}
