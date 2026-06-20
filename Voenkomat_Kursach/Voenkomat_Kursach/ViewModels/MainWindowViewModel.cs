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
using Voenkomat_Kursach.DB;
using Voenkomat_Kursach.Models;
using Voenkomat_Kursach.Views;

namespace Voenkomat_Kursach.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    
    private MainWindow thiswin;//окно этой ВМ
    private Window nextWin;//окно выбора призывников
    private AppSettings _appSettings;
    private UserRepository _ur;

    public void SetWin(MainWindow win)
    {
        thiswin = win;
    }


    public MainWindowViewModel(IServiceProvider sp, AppSettings aps, UserRepository ur) : base(sp)
    {
        
        _sp = sp;
        _appSettings = aps;
        
        _ur = ur;


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
    private void FindUserr()
    {

        //тут надо получить пользователя из базы
        var user = GetUser();
        
        //если такой пользователь есть
        if (user.Id != -1)
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

    [RelayCommand]
    private void FindUser()
    {
        
        var user = GetUser();

        if (user.Id != 0)
        {
            
            Type userWinT;//сюда запишем тип окна пользователя
            Type userVmT;//а сюда ВМ для него
        
            //если пользователь является мед работником, значит ему нужно окно выбора призывников
            //user.Role.IsMed

            //проверка роли и сохранение типа 
            switch (_appSettings.Roles.GetValueOrDefault(user.Role.Name, ""))
            {

                case "admin":
                {
                    userWinT = typeof(AdminWIndow);
                    userVmT = typeof(AdminViewModel);
                    
                    break;
                }

                case "arch":
                {
                    userWinT = typeof(ArchiverWindow);
                    userVmT = typeof(ArchiverViewModel);
                    
                    break;
                }

                case "reg":
                {
                    userWinT = typeof(RegistratorWindow);
                    userVmT = typeof(RegistatorViewModel);
                    
                    break;
                }

                case "doctor":
                {
                    userWinT = typeof(DoctorWindow);
                    userVmT = typeof(DoctorViewModel);
                    
                    break;
                }

                case "comis":
                {
                    userWinT = typeof(ComissionWindow);
                    userVmT = typeof(ComissionViewModel);
                    
                    break;
                }

                default:
                {
                    BlinkLogin("нет такой роли в приложении, обратитесь к администратору базы данных, который плохо читал инструкцию", 3000);
                    
                    return;
                }
                
            }
            
            
            if (user.Role.IsMed)//если мед работник
            {
                nextWin = ActivatorUtilities.CreateInstance<RecruitChooseWindow>(_sp);
                nextWin.DataContext = ActivatorUtilities.CreateInstance<RecruitChooseViewModel>(_sp, nextWin, userWinT, userVmT, user);
            }
            else
            {
                nextWin = ActivatorUtilities.GetServiceOrCreateInstance(_sp, userWinT) as Window;
                nextWin.DataContext = ActivatorUtilities.CreateInstance(_sp, userVmT, nextWin, user) as UserBaseViewModel;
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
        thiswin.Close();

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

            u.Id = 1;

            u.Role.Name = Login;

            if (Login is "doc" or "med")
            {
                u.Role.IsMed = true;
                u.Employee.Job.Id = 4;
            }

        }
        
        return u;

    }

    private User GetUser() => _ur.GetUser(Login, Password);
    
}