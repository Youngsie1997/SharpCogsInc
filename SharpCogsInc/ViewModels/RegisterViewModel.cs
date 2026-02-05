using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SharpCogsInc.Models;
using SharpCogsInc.Services;

namespace SharpCogsInc.ViewModels;

public partial class RegisterViewModel : PageViewModel
{

    private readonly INavigationService _navigationService;
    private readonly ApiService _apiService;
    [ObservableProperty]
    private string? _username;
    [ObservableProperty]
    private string? _friendly;
    [ObservableProperty]
    private string? _password;
    public RegisterViewModel(INavigationService navigationService, ApiService apiService)
    {
        _navigationService = navigationService;
        _apiService = apiService;
        PageName = ApplicationPageNames.Register;
    }



    [RelayCommand]
    private async Task Register()
    {
        if (Username != null && Password != null && Friendly != null)
        {
                    ClashRegisterDto registerDto = new ClashRegisterDto
                    {
                        username = Username,
                        password = Password,
                        friendly = Friendly,
                    };
        
                    var response = await _apiService.RegisterClient(registerDto);

                    Debug.Assert(registerDto.username != null );
                    if (response.Id != null)
                    {
                        var account = new ClashAccount
                        {
                            Username = registerDto.username,
                            Token = response.Token,
                            Id = response.Id.Value
                        };
                        await FileService.SaveToFileAsync(account);
                        _navigationService.NavigateTo(ApplicationPageNames.Home);
                    }
        }
    }

    [RelayCommand]
    private void GoToHome()
    {
       _navigationService.NavigateTo(ApplicationPageNames.Home); 
    }
        }

