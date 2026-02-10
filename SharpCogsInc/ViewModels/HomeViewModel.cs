using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SharpCogsInc.Models;
using SharpCogsInc.Services;

namespace SharpCogsInc.ViewModels;

public partial class HomeViewModel : PageViewModel
{
   private readonly INavigationService _navigationService;
   [ObservableProperty]
   private int _progress;

   [ObservableProperty]
   private ObservableCollection<ClashAccount> _clashAccounts = new ObservableCollection<ClashAccount>();
   [ObservableProperty]
   private ClashAccount _selectedClashAccount;

   private readonly LinuxSettings  _launcherSettings;
   private readonly ApiService _apiService;



   public HomeViewModel(INavigationService navigationService, ApiService apiService, LinuxSettings launcherSettings)
   {
      PageName = ApplicationPageNames.Home;
      _navigationService = navigationService;
      var accountsLoaded =  FileService.LoadFromFile();
      _apiService = apiService;
      _launcherSettings = launcherSettings;
      if (accountsLoaded is not { Count: > 0 }) return;
      foreach (var account in accountsLoaded.OfType<ClashAccount>())
      {
          _clashAccounts.Add(account);
      }

   }


   [RelayCommand]
   private void GoToRegister()
   {
       Console.WriteLine(SelectedClashAccount);
     _navigationService.NavigateTo(ApplicationPageNames.Register); 
   }

   [RelayCommand]
   private void GoToSettings()
   {
       _navigationService.NavigateTo(ApplicationPageNames.Settings);
   }

   [RelayCommand]
   private async Task GetMetaData()
   {
       var patchManifest = await _apiService.GetClashManifests(SelectedClashAccount.Token);

       if (patchManifest is not null)
       {
           var filepaths = await FileService.GetFilePaths(patchManifest.Files);

           foreach (var file in filepaths)
           {
               Console.WriteLine(file.Item1);
           }
           
       }

       if (_launcherSettings.WineBinary != null)
           Console.WriteLine(await FileService.GetSha1FileHash(_launcherSettings.WineBinary));
   }


}