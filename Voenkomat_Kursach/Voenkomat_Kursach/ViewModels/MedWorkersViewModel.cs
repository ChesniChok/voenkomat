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
        
        var chooseWin = ActivatorUtilities.CreateInstance<RecruitChooseWindow>(_sp);
        var vm = ActivatorUtilities.CreateInstance<RecruitChooseViewModel>(_sp, chooseWin, _win.GetType(), this.GetType(), _user);
        
        chooseWin.DataContext = vm;

        chooseWin.Position = _win.Position;

        chooseWin.Show();
        _win.Close();
        
    }

    protected override void GoBack() => GoToChoose();
    
}