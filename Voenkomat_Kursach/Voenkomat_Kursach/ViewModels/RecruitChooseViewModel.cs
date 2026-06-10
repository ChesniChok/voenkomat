using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.ViewModels;

public partial class RecruitChooseViewModel : ViewModelBase
{

    private Window _thisWin;
    private Window _nextWin;

    public RecruitChooseViewModel(IServiceProvider sp, Window thisWin, Window nextWin) : base(sp)
    {
        
        _sp = sp;

        _thisWin = thisWin;
        _nextWin = nextWin;

        Start();
        
    }

    private void Start()
    {
        
        GoToNextWinText = "Выбрать";
        SearchString = "";

        FillRecruits();
        
    }
    
    private void FillRecruits()
    {

        Recruits = new ObservableCollection<Recruit>();
        
        var r = new Recruit();
        r.Id = -3;
        
        Recruits.Add(new());
        Recruits.Add(new());
        Recruits.Add(new());
        Recruits.Add(new());
        Recruits.Add(new());
        Recruits.Add(r);
        
    }


    [ObservableProperty] private string _searchString;

    [ObservableProperty] private string _goToNextWinText;
    
    [ObservableProperty] private ObservableCollection<Recruit> _recruits;
    [ObservableProperty] private Recruit _selectedRecruit;


    protected override void GoBack()
    {
        _nextWin.Close();
        
        GoToMain(_thisWin);
    }


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

            _nextWin.Position = _thisWin.Position;
            
            (_nextWin.DataContext as MedWorkersViewModel)?.SetRec(SelectedRecruit);
            
            _nextWin.Show();
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