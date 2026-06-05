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
    private MainWindow thiswin;//окно этой ВМ
    private Window nextWin;//окно выбора призывников

    public void SetWin(MainWindow win)
    {
        thiswin = win;
    }


    public MainWindowViewModel(IServiceProvider sp)
    {
        
        _sp = sp;

        Start();

    }

    public void Start()
    {

        Login = "";
        Password = "";
        
        LoginText = "Войти в учётную запись";
        
    }


    [ObservableProperty] private string login;
    [ObservableProperty] private string password;

    [ObservableProperty] private string _loginText;


    [RelayCommand]
    private void FindUser()
    {

        var user = new User();//тут получаем юзера из базы
        user.Role.Name = Login;
        user.Role.IsMed = true;
        
        //если такой пользователь есть
        if (user != null)
        {

            Window userWin;
            UserBaseViewModel userVm;
            
            Window userBackWin;
            
            
            if (user.Role.IsMed)//если роль человека связана с медкомиссией
            {
                Window chooseWin = ActivatorUtilities.CreateInstance<RecruitChooseWindow>(_sp);//то ему нужно окно выбора призывника
                
                userBackWin = chooseWin;
            }
            else
            {
                userBackWin = thiswin;
            }
            
            
            switch (user.Role.Name)//проверяем роль и выдаём соответствующее окно
            {

                case "Администратор":
                {
                    userWin = ActivatorUtilities.CreateInstance<AdminWIndow>(_sp);
                    userVm = ActivatorUtilities.CreateInstance<AdminViewModel>(_sp, user, userWin, userBackWin);

                    break;
                }
                
                case "Архивариус":
                {
                    userWin = ActivatorUtilities.CreateInstance<ArchiverWindow>(_sp);
                    userVm = ActivatorUtilities.CreateInstance<ArchiverViewModel>(_sp, user, userWin, userBackWin);

                    break;
                }
                
                case "Регистрирующий":
                {
                    userWin = ActivatorUtilities.CreateInstance<RegistratorWindow>(_sp);
                    userVm = ActivatorUtilities.CreateInstance<RegistatorViewModel>(_sp, user, userWin, userBackWin);

                    break;
                }
                
                case "Врач":
                {
                    userWin = ActivatorUtilities.CreateInstance<DoctorWindow>(_sp);
                    userVm = ActivatorUtilities.CreateInstance<DoctorViewModel>(_sp, user, userWin, userBackWin);

                    break;
                }
                
                case "Комиссионщик":
                {
                    userWin = ActivatorUtilities.CreateInstance<ComissionWindow>(_sp);
                    userVm = ActivatorUtilities.CreateInstance<ComissionViewModel>(_sp, user, userWin, userBackWin);

                    break;
                }
                
                default:
                {
                    //throw new ArgumentException("нет такой роли в приложении, обратитесь к администратору базы данных, который плохо читал инструкцию");
                    BlinkLogin();
                    return;
                }

            }
            userWin.DataContext = userVm;
            
            
            if (user.Role.IsMed)//если роль человека связана с медкомиссией
            {
                //создаём наконец ВМ для окна выбора
                userBackWin.DataContext = ActivatorUtilities.CreateInstance<RecruitChooseViewModel>(_sp, thiswin, userBackWin, userWin);//окно возврата, окно выбора, окно пользователя
                nextWin = userBackWin;
            }
            else
            {
                nextWin = userWin;
            }
            
            
            GoToNextWin();

        }
        else
        {
            BlinkLogin();
        }
        
    }

    private void GoToNextWin()
    {

        nextWin.Position = thiswin.Position;
        
        nextWin.Show();
        thiswin.Hide();

    }
    

    private async void BlinkLogin()
    {
        
        LoginText = "Учётная запись не найдена";

        await Task.Delay(1000);
        
        LoginText = "Войти в учётную запись";
        
    }

}