using System;
using Avalonia.Controls;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.ViewModels;

public abstract class UserBaseViewModel : ViewModelBase
{
    
    protected User _user;
    protected Window _win;

    public UserBaseViewModel(IServiceProvider sp, User user, Window win) : base(sp)
    {
        _user = user;
        _win = win;
    }
    
}