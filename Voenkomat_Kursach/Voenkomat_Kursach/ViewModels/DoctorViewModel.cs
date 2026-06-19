using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.ViewModels;

public partial class DoctorViewModel : MedWorkersViewModel
{
    
    protected override void GoBack() => GoToChoose();


    public DoctorViewModel(User user, Window win, IServiceProvider sp, Recruit rec) : base(sp, user, win, rec)
    {
        
        
        CheckItems = new ObservableCollection<ChecklistItem>();
        checks();//тут получаем из базы
        
        DoktorAdditions =  new ObservableCollection<DoktorAdditions>();
        
        IsCheckEnded = false;
        IsAddsEnded = false;

    }
    
    
    [ObservableProperty] private ObservableCollection<ChecklistItem> _checkItems;
    
    [ObservableProperty] private ObservableCollection<DoktorAdditions> _doktorAdditions;

    [ObservableProperty] private bool _isCheckEnded;
    [ObservableProperty] private bool _isAddsEnded;

    private bool IsCheckRealyEnded()
    {
        var a = true;

        foreach (var item in CheckItems)
        {
            a &= item.IsChecked;
        }
        
        return a;
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
        DoktorAdditions.Add(new DoktorAdditions(DoktorAdditions.Count,""));
    }

    [RelayCommand]
    public void RemoveAddition(DoktorAdditions item)
    {
        DoktorAdditions.Remove(item);
    }


    [RelayCommand]
    public void End()//завершение 
    {

        if (IsCheckRealyEnded() && IsCheckEnded && IsAddsEnded)
        {
            
            _win.Hide();
            
        }
        
    }
    
}