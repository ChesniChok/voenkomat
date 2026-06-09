using System;
using Avalonia.Controls;
using Voenkomat_Kursach.Models;
using Voenkomat_Kursach.Views;

namespace Voenkomat_Kursach.ViewModels;

public class RegistatorViewModel : UserBaseViewModel
{
    public RegistatorViewModel(IServiceProvider sp, User user, Window win) : base(sp, user, win)
    {
    }
}