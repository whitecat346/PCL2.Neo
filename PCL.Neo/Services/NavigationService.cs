using CommunityToolkit.Mvvm.ComponentModel;
using PCL.Neo.ViewModels;
using PageViewModelBase = PCL.Neo.ViewModels.Pages.PageViewModelBase;

namespace PCL.Neo.Services;

public partial class NavigationService : ObservableObject
{
    private readonly Stack<PageViewModelBase> _backStack = [];
    private readonly Stack<PageViewModelBase> _forwardStack = [];

    [ObservableProperty]
    private PageViewModelBase? _currentPage;

    public void NavigateTo(PageViewModelBase page)
    {
        if (CurrentPage is not null)
        {
            _backStack.Push(CurrentPage);
        }

        _forwardStack.Clear();
        CurrentPage = page;
    }

    public void GoBack()
    {
        if (_backStack.Count > 0 && CurrentPage is not null)
        {
            _forwardStack.Push(CurrentPage);
            CurrentPage = _backStack.Pop();
        }
    }

    public void GoForward()
    {
        if (_forwardStack.Count > 0 && CurrentPage is not null)
        {
            _backStack.Push(CurrentPage);
            CurrentPage = _forwardStack.Pop();
        }
    }
}
