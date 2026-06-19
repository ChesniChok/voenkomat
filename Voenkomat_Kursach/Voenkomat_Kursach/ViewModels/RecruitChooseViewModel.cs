using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Voenkomat_Kursach.DB;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.ViewModels;

public partial class RecruitChooseViewModel : ViewModelBase
{

    private Window _thisWin;
    
    private Type _winT;
    private Type _vmT;
    private User _u;

    private RecruitRepository _rr;
    

    public RecruitChooseViewModel(IServiceProvider sp, RecruitRepository rr, Window thisWin, Type winT, Type vmT, User u) : base(sp)
    {
        
        _sp = sp;

        _thisWin = thisWin;
        
        _winT = winT;
        _vmT = vmT;
        _u = u;

        _rr = rr;

        Start();
        
    }

    private void Start()
    {
        
        GoToNextWinText = "Выбрать";
        SearchString = "";

        UpdateRecruits();
        
    }
    
    private void UpdateRecruits() => Recruits = new(_rr.GetAll(SearchString));


    [ObservableProperty] private string _searchString;

    [ObservableProperty] private string _goToNextWinText;
    
    [ObservableProperty] private ObservableCollection<Recruit> _recruits;
    [ObservableProperty] private Recruit _selectedRecruit;


    protected override void GoBack() => GoToMain(_thisWin);
    
    [RelayCommand] private void UpdateRecs() =>  UpdateRecruits();


    [RelayCommand]
    public void GoNext()
    {

        if (SelectedRecruit == null)
        {
            BlinkGoToNextWin(); 
            
            return;
        }
        
        if (_thisWin == null) return;

        if (SelectedRecruit?.Id != -1)
        {
            
            var win = ActivatorUtilities.CreateInstance(_sp, _winT) as Window;
            var vm = ActivatorUtilities.CreateInstance(_sp, _vmT, win, _u, SelectedRecruit) as MedWorkersViewModel;
            win.DataContext = vm;
            
            
            win.Position = _thisWin.Position;

            win.Show();
            _thisWin.Close();

        }
        else
        {
            BlinkGoToNextWin();
        }
        
    }
    
    private async void BlinkGoToNextWin()
    {
        
        GoToNextWinText = "Ошибка";

        await Task.Delay(1000);
        
        GoToNextWinText = "Выбрать";
        
    }
    



}