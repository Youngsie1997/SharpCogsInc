using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using SharpCogsInc.ViewModels;

namespace SharpCogsInc.Models;

public partial class PageViewModel : ViewModelBase
{
   [ObservableProperty]
   private ApplicationPageNames pageName;
}