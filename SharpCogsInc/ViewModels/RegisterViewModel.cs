using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SharpCogsInc.Models;
using SharpCogsInc.Services;
using SharpCogsInc.Views;

namespace SharpCogsInc.ViewModels;

public partial class RegisterViewModel : PageViewModel
{

    private readonly INavigationService _navigationService;
    public RegisterViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        PageName = ApplicationPageNames.Register;
    }



    [RelayCommand]
    private void Register() 
    {
        Console.WriteLine("HELLO FROM EL REG");
        _navigationService.NavigateTo(ApplicationPageNames.Home);
    }


}