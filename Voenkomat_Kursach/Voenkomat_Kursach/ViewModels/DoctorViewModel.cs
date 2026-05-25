using System;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.ViewModels;

public partial class DoctorViewModel : UserBaseViewModel
{

    private IServiceProvider _sp;


    public DoctorViewModel(User user, Window win, IServiceProvider sp) : base(user, win)
    {
        _sp = sp;
    }
    
}