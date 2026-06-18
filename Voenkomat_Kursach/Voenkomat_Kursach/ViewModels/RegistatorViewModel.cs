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
    private MedComissionRepository _mr;
    private VisitRepository _vr;
    
    public RegistatorViewModel(IServiceProvider sp, User user, Window win, RecruitRepository rr, MedComissionRepository mr, VisitRepository vr) : base(sp, user, win)
    {
        
        _rr = rr;
        _mr = mr;
        _vr = vr;
        
        Start();
        
    }

    private void Start()
    {
        UpdateRecs();
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
    
    [ObservableProperty] private MedComission _selectedCom;
    [ObservableProperty] private ObservableCollection<MedComission> _coms;
    [ObservableProperty] private int _comPage;
    [RelayCommand] private void UpdateComs() => Coms = new(_mr.GetPage(RecPage, 10, SelectedRec));

    [RelayCommand]
    private void FinishCom()
    {
        SelectedCom.EndDate = DateOnly.FromDateTime(DateTime.Now);
        _mr.Update(SelectedCom);
        UpdateComs();
    }
    [RelayCommand]
    public void NextPageCom()
    {
        if (ComPage == _mr.Count() / 10) return;
        ComPage += 10;
        UpdateComs();
    }
    [RelayCommand]
    public void PrevPageCom()
    {
        if (ComPage == 0) return;
        ComPage -= 10;
        UpdateComs();
    }
    [RelayCommand]
    public void FirstPageCom()
    {
        ComPage = 0;
        UpdateComs();
    }
    [RelayCommand]
    public void LastPageCom()
    {
        ComPage = (_mr.Count()-1) / 10 * 10;
        UpdateComs();
    }
    [RelayCommand] public void AddCom()
    {
        var com = new MedComission();
        com.Recruit = SelectedRec;
        _mr.Add(com);
        UpdateComs();
    }
    [RelayCommand] public void UpdateCom()
    {
        _mr.Update(SelectedCom);
        UpdateComs();
    }
    [RelayCommand] public void DeleteCom()
    {
        _mr.Delete(SelectedCom);
        UpdateComs();
    }
    
    [ObservableProperty] private Visit _selectedVis;
    [ObservableProperty] private ObservableCollection<Visit> _viss;
    [ObservableProperty] private int _visPage;
    [RelayCommand] private void UpdateViss() => Viss = new(_vr.GetPage(VisPage, 10, SelectedCom));
    [RelayCommand]
    private void FinishVis()
    {
        SelectedVis.OutTime = TimeOnly.FromDateTime(DateTime.Now);
        _vr.Update(SelectedVis);
        UpdateViss();
    }
    [RelayCommand]
    public void NextPageVis()
    {
        if (VisPage == _vr.Count() / 10) return;
        VisPage += 10;
        UpdateViss();
    }
    [RelayCommand]
    public void PrevPageVis()
    {
        if (VisPage == 0) return;
        VisPage -= 10;
        UpdateViss();
    }
    [RelayCommand]
    public void FirstPageVis()
    {
        VisPage = 0;
        UpdateViss();
    }
    [RelayCommand]
    public void LastPageVis()
    {
        VisPage = (_vr.Count()-1) / 10 * 10;
        UpdateViss();
    }
    [RelayCommand] public void AddVis()
    {
        var vis = new Visit();
        vis.MedComission = SelectedCom;
        _vr.Add(vis);
        UpdateViss();
    }
    [RelayCommand] public void UpdateVis()
    {
        _vr.Update(SelectedVis);
        UpdateVis();
    }
    [RelayCommand] public void DeleteVis()
    {
        _vr.Delete(SelectedVis);
        UpdateViss();
    }
    
    
    
    protected override void GoBack() => GoToMain(_win);
    
}