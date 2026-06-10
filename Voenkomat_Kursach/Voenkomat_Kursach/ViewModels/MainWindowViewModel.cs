using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Voenkomat_Kursach.Models;
using Voenkomat_Kursach.Views;

namespace Voenkomat_Kursach.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    
    private MainWindow thiswin;//окно этой ВМ
    private Window nextWin;//окно выбора призывников
    private AppSettings _appSettings;

    public void SetWin(MainWindow win)
    {
        thiswin = win;
    }


    public MainWindowViewModel(IServiceProvider sp, IOptions<AppSettings> aps) : base(sp)
    {
        
        _sp = sp;
        _appSettings = aps.Value;
        

        Start();

    }

    public void Start()
    {

        Login = "";
        Password = "";
        
        LoginText = "Войти в учётную запись";
        
    }

    protected override void GoBack() => thiswin.Close();


    [ObservableProperty] private string login;
    [ObservableProperty] private string password;

    [ObservableProperty] private string _loginText;


    [RelayCommand]
    private void FindUser()
    {

        //тут надо получить пользователя из базы
        var user = GenerateUser();
        
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
            
            
            switch (_appSettings.Roles.GetValueOrDefault(user.Role.Name, ""))//проверяем роль и выдаём соответствующее окно
            {

                case "admin":
                {
                    userWin = ActivatorUtilities.CreateInstance<AdminWIndow>(_sp);
                    userVm = ActivatorUtilities.CreateInstance<AdminViewModel>(_sp, user, userWin);

                    break;
                }
                
                case "arch":
                {
                    userWin = ActivatorUtilities.CreateInstance<ArchiverWindow>(_sp);
                    userVm = ActivatorUtilities.CreateInstance<ArchiverViewModel>(_sp, user, userWin);

                    break;
                }
                
                case "reg":
                {
                    userWin = ActivatorUtilities.CreateInstance<RegistratorWindow>(_sp);
                    userVm = ActivatorUtilities.CreateInstance<RegistatorViewModel>(_sp, user, userWin);

                    break;
                }
                
                case "doctor":
                {
                    userWin = ActivatorUtilities.CreateInstance<DoctorWindow>(_sp);
                    userVm = ActivatorUtilities.CreateInstance<DoctorViewModel>(_sp, user, userWin);

                    break;
                }
                
                case "comis":
                {
                    userWin = ActivatorUtilities.CreateInstance<ComissionWindow>(_sp);
                    userVm = ActivatorUtilities.CreateInstance<ComissionViewModel>(_sp, user, userWin);

                    break;
                }
                
                default:
                {
                    //throw new ArgumentException();
                    BlinkLogin("нет такой роли в приложении, обратитесь к администратору базы данных, который плохо читал инструкцию", 3000);
                    return;
                }

            }
            userWin.DataContext = userVm;
            
            
            if (user.Role.IsMed)//если роль человека связана с медкомиссией
            {
                //создаём наконец ВМ для окна выбора
                userBackWin.DataContext = ActivatorUtilities.CreateInstance<RecruitChooseViewModel>(_sp, userBackWin, userWin);//окно возврата, окно выбора, окно пользователя
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
    

    private async void BlinkLogin(string text = "Учётная запись не найдена", int delay = 1000)
    {
        
        LoginText = text;

        await Task.Delay(delay);
        
        LoginText = "Войти в учётную запись";
        
    }


    private User GenerateUser()
    {

        var u = new User();

        if (Login != "")
        {

            u.Role.Name = Login;

            if (Login is "Врач" or "Комиссионщик")
            {
                u.Role.IsMed = true;
            }

        }
        
        return u;

    }

}