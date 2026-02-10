using System.Threading.Tasks;
using SharpCogsInc.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SharpCogsInc.Services;

namespace SharpCogsInc.ViewModels;

public partial class SettingsViewModel : PageViewModel
{
    private readonly IFolderPickerService _folderPicker;
    [ObservableProperty]
    private LinuxSettings _launcherSettings;
    


    public SettingsViewModel(IFolderPickerService folderPicker, LinuxSettings launcherSettings)
    {
        PageName = ApplicationPageNames.Settings;
        _folderPicker = folderPicker;
        _launcherSettings = launcherSettings;

        
        _downloadPath = _launcherSettings.DownloadPath;
        _wineprefix = _launcherSettings.WinePrefix;
        _winebinary = _launcherSettings.WineBinary;

    }
    
    [ObservableProperty]
    private string? _downloadPath;

    [ObservableProperty]
    private string? _winebinary;


    [ObservableProperty]
    private string? _wineprefix;
    
    

    [RelayCommand]
    private async Task SelectDownloadFolder()
    {
        var path = await _folderPicker.PickFolderAsync("Choose a Download Folder");
        if (!string.IsNullOrEmpty(path))
        {
            DownloadPath = path;

            LauncherSettings.DownloadPath = path;
            
            await FileService.SaveSettingsToDisk(LauncherSettings);
        }
        

    }

    [RelayCommand]
    private async Task SelectWineBinary()
    {
        var path = await _folderPicker.PickFileAsync("Choose a Wine Binary");
        if (!string.IsNullOrEmpty(path))
        {
            Winebinary = path;
            LauncherSettings.WineBinary = path;

            await FileService.SaveSettingsToDisk(LauncherSettings);
        }
    }
    
    
    [RelayCommand]
    private async Task SelectWinePrefix()
    {
        var path = await _folderPicker.PickFolderAsync("Choose a wine prefix  folder");
        if (!string.IsNullOrEmpty(path))
        {
            Wineprefix = path;
            LauncherSettings.WinePrefix = path;
            
            await FileService.SaveSettingsToDisk(LauncherSettings);
        }
        

    }
    
   


    
    
    
}