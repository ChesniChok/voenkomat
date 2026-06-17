using System;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Voenkomat_Kursach.DB;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.ViewModels;

public partial class RegistatorViewModel : UserBaseViewModel
{

    private RecruitRepository _rr;
    
    public RegistatorViewModel(IServiceProvider sp, User user, Window win, RecruitRepository rr) : base(sp, user, win)
    {
        
        _rr = rr;
        
        Start();
        
    }

    private void Start()
    {
        Recs = new(_rr.GetPage(0, 10));
    }


    [ObservableProperty] private string _search;

    [ObservableProperty] private Recruit _selectedRec;
    [ObservableProperty] private ObservableCollection<Recruit> _recs;
    [ObservableProperty] private int _recPage;
    [RelayCommand] private void UpdateRecs()
    {
        
        ObservableCollection<Recruit> got;

        if (String.IsNullOrEmpty(Search))
        {
            got = new(_rr.GetPage(RecPage, 10));
        }
        else
        {
            got = new(_rr.GetPage(RecPage, 10, Search));
        }

        Recs = got;

    }

    [RelayCommand]
    public void NextPageRec()
    {
        if (RecPage == _rr.Count() / 10) return;
        RecPage += 10;
        UpdateRecs();
    }
    [RelayCommand]
    public void PrevPageRec()
    {
        if (RecPage == 0) return;
        RecPage -= 10;
        UpdateRecs();
    }
    [RelayCommand]
    public void FirstPageRec()
    {
        RecPage = 0;
        UpdateRecs();
    }
    [RelayCommand]
    public void LastPageRec()
    {
        RecPage = (_rr.Count()-1) / 10 * 10;
        UpdateRecs();
    }
    [RelayCommand] public void AddRec()
    {
        _rr.Add(new());
        UpdateRecs();
    }
    [RelayCommand] public void UpdateRec()
    {
        _rr.Update(SelectedRec);
        UpdateRecs();
    }
    [RelayCommand] public void DeleteRec()
    {
        _rr.Delete(SelectedRec);
        UpdateRecs();
    }
    
    
    
    protected override void GoBack() => GoToMain(_win);
    
}