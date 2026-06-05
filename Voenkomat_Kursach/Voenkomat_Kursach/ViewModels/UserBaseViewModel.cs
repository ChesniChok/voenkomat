using System;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using Voenkomat_Kursach.Models;
using Voenkomat_Kursach.Views;

namespace Voenkomat_Kursach.ViewModels;

public abstract partial class UserBaseViewModel : ViewModelBase
{
    
    protected User _user;
    protected Window _win;
    protected Recruit _rec;
    protected Window _backWin;

    public UserBaseViewModel(IServiceProvider sp, User user, Window win, Window backWin) : base(sp)
    {
        _user = user;
        _win = win;
        _backWin = backWin;
    }

    public void SetRec(Recruit rec)
    {
        _rec = rec;
    }
    
    [RelayCommand]
    protected void GoBack()
    {
        
        _backWin.Position = _win.Position;

        _backWin.Show();
        
        _win.Hide();
        
    }
    
}