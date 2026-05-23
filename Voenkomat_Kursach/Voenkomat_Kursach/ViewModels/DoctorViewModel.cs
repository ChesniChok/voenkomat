using System;
using Avalonia.Controls;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.ViewModels;

public class DoctorViewModel : UserBaseViewModel
{

    private IServiceProvider _sp;


    public DoctorViewModel(User user, Window win, IServiceProvider sp) : base(user, win)
    {
        _sp = sp;
    }
}