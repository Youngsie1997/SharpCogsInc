using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SharpCogsInc.Models;
using SharpCogsInc.Services;

namespace SharpCogsInc.ViewModels;

public partial class HomeViewModel : PageViewModel
{
   [ObservableProperty]
   private string _title;
   private INavigationService _navigationService;



   public HomeViewModel(INavigationService navigationService)
   {
      PageName = ApplicationPageNames.Home;
      _navigationService = navigationService;
   }


   [RelayCommand]
   private void GoToRegister()
   {
     _navigationService.NavigateTo(ApplicationPageNames.Register); 
   }
}