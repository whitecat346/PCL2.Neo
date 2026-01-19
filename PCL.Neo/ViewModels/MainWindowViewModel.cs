using CommunityToolkit.Mvvm.Input;
using PCL.Neo.Services;

namespace PCL.Neo.ViewModels;

public partial class MainWindowViewModel(NavigationService navService) : ViewModelBase
{
    public NavigationService NavService { get; } = navService;

    [RelayCommand]
    private void GoBack()
    {
        NavService.GoBack();
    }

    [RelayCommand]
    private void GoForward()
    {
        NavService.GoForward();
    }
}
