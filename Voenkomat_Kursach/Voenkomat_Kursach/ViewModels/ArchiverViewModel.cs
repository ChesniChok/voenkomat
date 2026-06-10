using System;
using Avalonia.Controls;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.ViewModels;

public class ArchiverViewModel : UserBaseViewModel
{
    
    public ArchiverViewModel(IServiceProvider sp, User user, Window win) : base(sp, user, win) {}
    
    
    protected override void GoBack() => GoToMain(_win);
    
}