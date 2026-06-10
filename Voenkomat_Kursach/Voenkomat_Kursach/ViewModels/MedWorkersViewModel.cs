using System;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using Voenkomat_Kursach.Models;
using Voenkomat_Kursach.Views;

namespace Voenkomat_Kursach.ViewModels;

public abstract class MedWorkersViewModel : UserBaseViewModel
{

    private Recruit _rec;
    
    
    public MedWorkersViewModel(IServiceProvider sp, User user, Window win) : base(sp, user, win) {}
    
    
    public void SetRec(Recruit rec) => _rec = rec;
    
    protected void GoToChoose()
    {
        
        var win = ActivatorUtilities.CreateInstance<RecruitChooseWindow>(_sp);
        var vm = ActivatorUtilities.CreateInstance<RecruitChooseViewModel>(_sp, win, _win);
        
        win.DataContext = vm;

        _win.Hide();
        win.Show();
        
    }

    protected override void GoBack() => GoToChoose();
    
}