using System;
using SharpCogsInc.Models;

namespace SharpCogsInc.Factories;

public class PageFactory(Func<ApplicationPageNames, PageViewModel> factory)
{
    public PageViewModel GetPageViewModel(ApplicationPageNames pageName) => factory.Invoke(pageName);
}