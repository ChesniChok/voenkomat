using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Voenkomat_Kursach.Models;
using Voenkomat_Kursach.Views;

namespace Voenkomat_Kursach.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    
    private IServiceProvider _sp;
    public MainWindow thiswin;
    private Window nextWin;
    
    
    public MainWindowViewModel(IServiceProvider sp)
    {
        
        _sp = sp;

        LoginText = "Войти в учётную запись";
        GoToNextWinText = "Выбрать";

        Recruits = new ObservableCollection<Recruit>();
        
        var r = new Recruit();
        r.Id = -3;
        
        Recruits.Add(new());
        Recruits.Add(new());
        Recruits.Add(new());
        Recruits.Add(new());
        Recruits.Add(new());
        Recruits.Add(r);

        FirstSelected = true;

    }


    [ObservableProperty] private string login;
    [ObservableProperty] private string password;

    [ObservableProperty] private string _loginText;
    [ObservableProperty] private string _goToNextWinText;


    [ObservableProperty] private string searchString;
    [ObservableProperty] private ObservableCollection<Recruit> recruits;
    
    [ObservableProperty] private Recruit selectedRecruit;

    [ObservableProperty] private bool firstSelected;
    [ObservableProperty] private bool secondSelected;


    [RelayCommand]
    private void FindUser()
    {

        var user = new User();//тут получаем юзера из базы
        user.Id = -3;
        user.Role.Name = "Врач";
        
        //если такой пользователь есть
        if (user.Id != -1)
        {

            Window win;
            UserBaseViewModel vm;
            
            switch (user.Role.Name)//проверяем роль
            {

                case "Администратор":
                {
                    win = ActivatorUtilities.CreateInstance<AdminWIndow>(_sp);
                    vm = ActivatorUtilities.CreateInstance<AdminViewModel>(_sp, user, win);

                    break;
                }
                
                case "Архивариус":
                {
                    win = ActivatorUtilities.CreateInstance<ArchiverWindow>(_sp);
                    vm = ActivatorUtilities.CreateInstance<ArchiverViewModel>(_sp, user, win);

                    break;
                }
                
                case "Регистрирующий":
                {
                    win = ActivatorUtilities.CreateInstance<RegistratorWindow>(_sp);
                    vm = ActivatorUtilities.CreateInstance<RegistatorViewModel>(_sp, user, win);

                    break;
                }
                
                case "Врач":
                {
                    win = ActivatorUtilities.CreateInstance<DoctorWindow>(_sp);
                    vm = ActivatorUtilities.CreateInstance<DoctorViewModel>(_sp, user, win);

                    break;
                }
                
                case "Комиссионщик":
                {
                    win = ActivatorUtilities.CreateInstance<ComissionWindow>(_sp);
                    vm = ActivatorUtilities.CreateInstance<ComissionViewModel>(_sp, user, win);

                    break;
                }
                
                default:
                {
                    throw new ArgumentException("нет такой роли в приложении, обратитесь к администратору базы данных, который плохо читал инструкцию");
                }

            }

            win.DataContext = vm;
            
            nextWin = win;

            SecondSelected = true;

        }
        else
        {
            BlinkLogin();
        }
        
    }

    [RelayCommand]
    public void GoToNextWin()
    {

        if (SelectedRecruit == null)
        {
            BlinkGoToNextWin(); 
            
            return;
        }

        if (SelectedRecruit?.Id != -1)
        {

            nextWin.Position = thiswin.Position;
            
            (nextWin.DataContext as UserBaseViewModel)?.SetRec(SelectedRecruit);
            
            nextWin.Show();
            thiswin.Close();

        }
        else
        {
            BlinkGoToNextWin();
        }
        
    }

    private async void BlinkLogin()
    {
        
        LoginText = "Учётная запись не найдена";

        await Task.Delay(1000);
        
        LoginText = "Войти в учётную запись";
        
    }
    
    private async void BlinkGoToNextWin()
    {
        
        GoToNextWinText = "Ошибка";

        await Task.Delay(1000);
        
        GoToNextWinText = "Выбрать";
        
    }

}