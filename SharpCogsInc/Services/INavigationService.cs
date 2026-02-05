using System;
using Avalonia;
using SharpCogsInc.Factories;
using SharpCogsInc.Models;

namespace SharpCogsInc.Services;

public interface INavigationService
{
    event Action<PageViewModel> NavigationRequested;
    void NavigateTo(ApplicationPageNames page);
    
}