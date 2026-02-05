using System;
using SharpCogsInc.Factories;
using SharpCogsInc.Models;

namespace SharpCogsInc.Services;

public class NavigationService : INavigationService
{
    private readonly PageFactory _pageFactory;
    public event Action<PageViewModel> NavigationRequested;


    public NavigationService(PageFactory pageFactory)
    {
        _pageFactory = pageFactory;
    }


    public void NavigateTo(ApplicationPageNames page)
    {
        var viewModel = _pageFactory.GetPageViewModel(page);
        NavigationRequested?.Invoke(viewModel);
    }
}