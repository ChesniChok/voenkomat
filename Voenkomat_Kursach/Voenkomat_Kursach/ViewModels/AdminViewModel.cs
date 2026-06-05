using System;
using Avalonia.Controls;
using Voenkomat_Kursach.Models;
using Voenkomat_Kursach.Views;

namespace Voenkomat_Kursach.ViewModels;

public class AdminViewModel : UserBaseViewModel
{
    public AdminViewModel(IServiceProvider sp, User user, Window win, Window backWin) : base(sp, user, win, backWin)
    {
    }
}