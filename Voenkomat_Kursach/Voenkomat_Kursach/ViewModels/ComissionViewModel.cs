using System;
using Avalonia.Controls;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.ViewModels;

public partial class ComissionViewModel : MedWorkersViewModel
{
    
    public ComissionViewModel(IServiceProvider sp, User user, Window win) : base(sp, user, win) {}


    protected override void GoBack() => GoToChoose();
    
}