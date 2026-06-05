using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Voenkomat_Kursach.Models;
using Voenkomat_Kursach.Views;

namespace Voenkomat_Kursach.ViewModels;

public partial class RecruitChooseViewModel : ViewModelBase
{
    
    private IServiceProvider _sp;

    private MainWindow _backWin;
    private Window _thisWin;
    private Window _nextWin;

    public RecruitChooseViewModel(IServiceProvider sp, MainWindow backWin, Window thisWin, Window nextWin)
    {
        
        _sp = sp;

        _backWin = backWin;
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



    [RelayCommand]
    public void GoBack()
    {
        
        _backWin.Position = _thisWin.Position;
        
        (_backWin.DataContext as MainWindowViewModel)?.Start();
        _backWin.Show();
        
        _thisWin.Close();
        _nextWin.Close();
        
    }
    


    [RelayCommand]
    public void GoToNextWin()
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
            
            (_nextWin.DataContext as UserBaseViewModel)?.SetRec(SelectedRecruit);
            
            _nextWin.Show();
            _thisWin.Hide();

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