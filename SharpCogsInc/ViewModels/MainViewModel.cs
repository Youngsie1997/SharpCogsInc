using System.Reflection;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SharpCogsInc.Models;
using SharpCogsInc.Services;

namespace SharpCogsInc.ViewModels;

public partial class MainViewModel : PageViewModel
{

    [ObservableProperty]
    private PageViewModel _currentPage;
    private readonly INavigationService _navigationService;

    public MainViewModel(INavigationService navigationService)
    {
       _navigationService = navigationService;

       _navigationService.NavigationRequested += OnNavigationRequested;
       
       navigationService.NavigateTo(ApplicationPageNames.Home);

    }

    private void OnNavigationRequested(PageViewModel newPage)
    {
        CurrentPage = newPage;
    }


    [RelayCommand]
    private void GoToSettings()
    {
        _navigationService.NavigateTo(ApplicationPageNames.Settings);
    }

    [RelayCommand]
    private void GoToHome()
    {
        _navigationService.NavigateTo(ApplicationPageNames.Home);
    }

    [RelayCommand]
    private void GoToRegister()
    {
        _navigationService.NavigateTo(ApplicationPageNames.Register);
    }

    
}