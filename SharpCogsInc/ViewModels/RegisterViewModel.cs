using System;
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
    private string? _username = string.Empty;

    [ObservableProperty]
    private string _friendly = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    public RegisterViewModel(INavigationService navigationService, ApiService apiService)
    {
        _navigationService = navigationService;
        _apiService = apiService;
        PageName = ApplicationPageNames.Register;
    }


    [RelayCommand]
    private async Task Register()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(Friendly))
            {
                return;
            }

            var registerDto = new ClashRegisterDto
            {
                Username = Username,
                Password = Password,
                Friendly = Friendly,
            };
            var response = await _apiService.RegisterClient(registerDto);
            if (response?.Status is null or false)
                throw new RegisterException();
            {
                var account = new ClashAccount
                {
                    Username = registerDto.Username,
                    Token = response.Token,
                    Id = response.Id
                };
                await FileService.SaveToFileAsync(account);

                _navigationService.NavigateTo(ApplicationPageNames.Home);
            }
        }
        catch (RegisterException)
        {
            // Display message to user
            Console.WriteLine("Register failed check username,password and friendly");
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            throw;
        }
    }

    [RelayCommand]
    private void GoToHome()
    {
        _navigationService.NavigateTo(ApplicationPageNames.Home);
    }
}