using System;
using System.Collections.ObjectModel;
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
   [ObservableProperty]
   private int _progress;

   [ObservableProperty]
   private ObservableCollection<ClashAccount> _clashAccounts = new ObservableCollection<ClashAccount>();
   [ObservableProperty]
   private ClashAccount _selectedClashAccount;
   
   private readonly ApiService _apiService;



   public HomeViewModel(INavigationService navigationService, ApiService apiService)
   {
      PageName = ApplicationPageNames.Home;
      _navigationService = navigationService;
      var accountsLoaded =  FileService.LoadFromFile();
      _apiService = apiService;

      if (accountsLoaded.Count > 0)
      {
          foreach (var account in accountsLoaded)
          {
              _clashAccounts.Add(account);
          }
      }

   }


   [RelayCommand]
   private void GoToRegister()
   {
       Console.WriteLine(SelectedClashAccount);
     _navigationService.NavigateTo(ApplicationPageNames.Register); 
   }

   [RelayCommand]
   private async void GetMetaData()
   {
       var manifest = await _apiService.GetManifestAsync(_selectedClashAccount.Token);
       foreach (var file in manifest.Files) 
       {
          Console.WriteLine(file); 
       }
       
   }

}