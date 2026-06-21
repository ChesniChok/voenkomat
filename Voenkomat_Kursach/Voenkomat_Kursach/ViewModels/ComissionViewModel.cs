using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Voenkomat_Kursach.DB;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.ViewModels;

public partial class ComissionViewModel : MedWorkersViewModel
{

    private MedComissionRepository _mr;
    private RecordRepository _rr;

    private MedComission _currentMedcomission;
    
    public ComissionViewModel(IServiceProvider sp, MedComissionRepository mr, RecordRepository rr, User user, Window win, Recruit rec) : base(sp, mr, user, win, rec)
    {
        
        _mr = mr;
        _rr = rr;

        _currentMedcomission = _mr.GetRecsLast(rec);
        
        GetAllProperties();

    }


    private List<Record> _allRecords;
    private void GetRecords() => _allRecords = _rr.GetAllMed(_currentMedcomission);

    private List<Record> _allChecks;
    private List<Record> _allAdds;
    private List<Record> _allConclusions;
    private void GetChecks() => _allChecks = new(_allRecords.Where(r => r.Type == "осмотр"));
    private void GetAdds() => _allAdds = new(_allRecords.Where(r => r.Type == "дополнение"));
    private void GetConclusions() => _allConclusions = new(_allRecords.Where(r => r.Type == "заключение"));
    
    
    
    [ObservableProperty] private ObservableCollection<Record>? _terChecks;
    [ObservableProperty] private ObservableCollection<Record>? _terAdds;
    [ObservableProperty] private Record? _terConclusion;
    private void SetTerProperties()
    {
        var jobName = "терапевт";
        
        TerChecks = new(_allChecks.Where(c => c.Author?.Job.Name == jobName));
        TerAdds = new(_allAdds.Where(а => а.Author?.Job.Name == jobName));
        TerConclusion = _allConclusions.FirstOrDefault(c => c.Author?.Job.Name == jobName);
    }
    
    [ObservableProperty] private ObservableCollection<Record>? _otorChecks;
    [ObservableProperty] private ObservableCollection<Record>? _otorAdds;
    [ObservableProperty] private Record? _otorConclusion;
    private void SetOtorProperties()
    {
        var jobName = "оториноларинголог";
        
        OtorChecks = new(_allChecks.Where(c => c?.Author.Job.Name == jobName));
        OtorAdds = new(_allAdds.Where(а => а?.Author.Job.Name == jobName));
        OtorConclusion = _allConclusions.FirstOrDefault(c => c.Author?.Job.Name == jobName);
    }
    
    [ObservableProperty] private ObservableCollection<Record>? _psihChecks;
    [ObservableProperty] private ObservableCollection<Record>? _psihAdds;
    [ObservableProperty] private Record? _psihConclusion;
    private void SetPsihProperties()
    {
        var jobName = "психиатр";
        
        PsihChecks = new(_allChecks.Where(c => c.Author?.Job.Name == jobName));
        PsihAdds = new(_allAdds.Where(а => а.Author?.Job.Name == jobName));
        PsihConclusion = _allConclusions.FirstOrDefault(c => c.Author?.Job.Name == jobName);
    }
    
    [ObservableProperty] private ObservableCollection<Record>? _nevrChecks;
    [ObservableProperty] private ObservableCollection<Record>? _nevrAdds;
    [ObservableProperty] private Record? _nevrConclusion;
    private void SetNevrProperties()
    {
        var jobName = "невролог";
        
        NevrChecks = new(_allChecks.Where(c => c.Author?.Job.Name == jobName));
        NevrAdds = new(_allAdds.Where(а => а.Author?.Job.Name == jobName));
        NevrConclusion = _allConclusions.FirstOrDefault(c => c.Author?.Job.Name == jobName);
    }
    
    [ObservableProperty] private ObservableCollection<Record>? _hirChecks;
    [ObservableProperty] private ObservableCollection<Record>? _hirAdds;
    [ObservableProperty] private Record? _hirConclusion;
    private void SetHirProperties()
    {
        var jobName = "хирург";
        
        HirChecks = new(_allChecks.Where(c => c.Author?.Job.Name == jobName));
        HirAdds = new(_allAdds.Where(а => а.Author?.Job.Name == jobName));
        HirConclusion = _allConclusions.FirstOrDefault(c => c.Author?.Job.Name == jobName);
    }
    
    [ObservableProperty] private ObservableCollection<Record>? _stomChecks;
    [ObservableProperty] private ObservableCollection<Record>? _stomAdds;
    [ObservableProperty] private Record? _stomConclusion;
    private void SetStomProperties()
    {
        var jobName = "стоматолог";
        
        StomChecks = new(_allChecks.Where(c => c.Author?.Job.Name == jobName));
        StomAdds = new(_allAdds.Where(а => а.Author?.Job.Name == jobName));
        StomConclusion = _allConclusions.FirstOrDefault(c => c.Author?.Job.Name == jobName);
    }
    
    [ObservableProperty] private ObservableCollection<Record>? _okulChecks;
    [ObservableProperty] private ObservableCollection<Record>? _okulAdds;
    [ObservableProperty] private Record? _okulConclusion;
    private void SetOkulProperties()
    {
        var jobName = "окулист";
        
        OkulChecks = new(_allChecks.Where(c => c.Author?.Job.Name == jobName));
        OkulAdds = new(_allAdds.Where(а => а.Author?.Job.Name == jobName));
        OkulConclusion = _allConclusions.FirstOrDefault(c => c.Author?.Job.Name == jobName);
    }

    private void GetAllProperties()
    {
        
        GetRecords();
        
        GetChecks();
        GetAdds();
        GetConclusions();
        
        SetTerProperties();
        SetPsihProperties();
        SetOtorProperties();
        SetHirProperties();
        SetStomProperties();
        SetOkulProperties();
        
    }

    [ObservableProperty] private string _category;
    
    [RelayCommand]
    public void FinishHim()
    {
        if (!string.IsNullOrEmpty(Category))
        {
            
            _currentMedcomission.EndDate = DateOnly.FromDateTime(DateTime.Now);
            _currentMedcomission.Category = Category;
        
            _mr.Update(_currentMedcomission);
        
            GoBack();
            
        }
    }
    


    protected override void GoBack() => GoToChoose();
    
}