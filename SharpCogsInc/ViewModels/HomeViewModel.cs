using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
   [ObservableProperty]
   private string _status;



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
       int progress = 0;

       if (patchManifest != null)
       {
           List<ClashFile> filesneeded = [];
            Status = "Validating Files";
           foreach (var file in patchManifest.Files)
           {
               {
                   var filepath = Path.Combine(_launcherSettings.DownloadPath ?? Environment.SpecialFolder.ApplicationData + "SharpCogsInc", file.FilePath);
                   if (File.Exists(filepath))
                   {
                       Status = $"Validating {file.FileName}";
                       var hashresult = await DownloadService.ValidateFile(file, filepath);

                       if (!hashresult.Success)
                       {
                           filesneeded.Add(file);
                       }
                   }
                   else
                   {
                       filesneeded.Add(file);
                   }
               }
           }

           var total  = filesneeded.Count;
           foreach (var file in filesneeded)
           {
               string downloadurl = $"https://r2prod.corporateclash.net/{await DownloadService.MakeObjectKey(file)}";
               Console.WriteLine(downloadurl);
               var filepath = Path.Combine(_launcherSettings.DownloadPath, file.FilePath.Replace("\\", "/") );

               Status = $"Downloading {file.FileName} {progress}/{total}";
               progress += 1;
               await DownloadService.DownloadAndDecompressGzipAsync(downloadurl, filepath);
           }
       }

       Status = "Done";
           
           
           }


}