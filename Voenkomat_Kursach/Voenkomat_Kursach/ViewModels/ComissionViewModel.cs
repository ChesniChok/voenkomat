using System;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Voenkomat_Kursach.DB;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.ViewModels;

public partial class ComissionViewModel : MedWorkersViewModel
{

    private MedComissionRepository _mr;
    private RecordRepository _rr;

    public ComissionViewModel(IServiceProvider sp, MedComissionRepository mr, RecordRepository rr, User user, Window win, Recruit rec) : base(sp, user, win, rec)
    {
        
        _mr = mr;
        _rr = rr;
        
    }



    [ObservableProperty] private ObservableCollection<Record> _hirChecks;
    [ObservableProperty] private ObservableCollection<Record> _hirAdds;


    protected override void GoBack() => GoToChoose();
    
}