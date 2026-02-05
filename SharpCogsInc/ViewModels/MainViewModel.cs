using CommunityToolkit.Mvvm.ComponentModel;
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
    


    
}