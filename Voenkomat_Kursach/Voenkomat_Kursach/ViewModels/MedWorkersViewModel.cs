using System;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Voenkomat_Kursach.Models;
using Voenkomat_Kursach.Views;

namespace Voenkomat_Kursach.ViewModels;

public abstract partial class MedWorkersViewModel : UserBaseViewModel
{

    [ObservableProperty] private Recruit _recruit;

    [ObservableProperty] private string _recFullName;


    public MedWorkersViewModel(IServiceProvider sp, User user, Window win, Recruit rec) : base(sp, user, win)
    {
        
        _recruit = rec;
        
        RecFullName = $"{Recruit.FamilyName} {Recruit.Name} {Recruit.FatherName}";
        
    }
    
    
    public void SetRec(Recruit rec) => Recruit = rec;
    
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