using System;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using Voenkomat_Kursach.Models;
using Voenkomat_Kursach.Views;

namespace Voenkomat_Kursach.ViewModels;

public partial class ComissionViewModel : UserBaseViewModel
{
    
    public ComissionViewModel(IServiceProvider sp, User user, Window win, Window backWin) : base(sp, user, win, backWin)
    {
        
    }
    
    
    
    
}