using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Voenkomat_Kursach.DB;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.ViewModels;

public partial class DoctorViewModel : MedWorkersViewModel
{
    
    protected override void GoBack() => GoToChoose();


    private ChecklistItemRepository _cr;
    private RecordRepository _rr;
    
    private MedComission _currentMedComission;

    public DoctorViewModel(IServiceProvider sp, MedComissionRepository mr, ChecklistItemRepository cr, RecordRepository rr, User user, Window win, Recruit rec) : base(sp, mr, user, win, rec)
    {
        
        _cr = cr;
        _rr = rr;
        
        CheckItems = new(_cr.GetAll(user.Employee.Job));
        
        DoctorAdditions =  new ObservableCollection<DoktorAddition>();
        
        IsCheckEnded = false;
        IsAddsEnded = false;

        _currentMedComission = _mr.GetRecsLast(rec);

    }
    
    
    [ObservableProperty] private ObservableCollection<ChecklistItem> _checkItems;
    
    [ObservableProperty] private ObservableCollection<DoktorAddition> _doctorAdditions;

    [ObservableProperty] private bool _isCheckEnded;
    [ObservableProperty] private bool _isAddsEnded;

    [ObservableProperty] private string _conclusion;

    private bool IsCheckRealyEnded
    {
        get
        {
            var a = true;

            foreach (var item in CheckItems)
            {
                a &= item.IsChecked;
            }
        
            return a;
        }
    }


    public void checks()
    {

        CheckItems.Add(new
        (
            -3,
            new(-3, "стоматолог"),
            "зубы",
            "все ли зубы на месте?"
        ));
        CheckItems.Add(new
        (
            -3,
            new(-3, "стоматолог"),
            "кариес",
            "есть ли кариес на зубах?\nномера зубов с кариесом."
        ));
        CheckItems.Add(new
        (
            -3,
            new(-3, "стоматолог"),
            "модификации",
            "есть ли модификации зубов:\nпломбы, протезы, брэкеты?\nномера зубов с модификациями."
        ));

    }


    [RelayCommand]
    public void AddAddition()
    {
        DoctorAdditions.Add(new DoktorAddition(DoctorAdditions.Count,""));
    }

    [RelayCommand]
    public void RemoveAddition(DoktorAddition item)
    {
        DoctorAdditions.Remove(item);
    }


    [RelayCommand]
    public void End()//завершение 
    {
        if (IsCheckRealyEnded && IsCheckEnded && IsAddsEnded && !String.IsNullOrEmpty(Conclusion))
        {

            foreach (var check in CheckItems)
            {
                _rr.Add(new(0, "осмотр", _user.Employee, _currentMedComission, check.Written, check.Description));
            }
            
            foreach (var addition in DoctorAdditions)
            {
                _rr.Add(new(0, "дополнения", _user.Employee, _currentMedComission, addition.Value, ""));
            }
            
            _rr.Add(new(0, "заключение", _user.Employee, _currentMedComission, Conclusion, "заключение"));
            
        }
    }
    
}