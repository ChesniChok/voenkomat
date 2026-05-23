using System;
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
    
    public MainWindowViewModel(IServiceProvider sp)
    {
        
        _sp = sp;

        ButtonText = "Войти в учётную запись";

    }


    [ObservableProperty] private string login;
    [ObservableProperty] private string password;

    [ObservableProperty] private string buttonText;


    [RelayCommand]
    private void LogIn()
    {

        var user = new User();//тут получаем юзера из базы
        user.Role.Name = "Доктор";
        
        //если такой пользователь есть
        if (/*user != null*/ true)
        {

            Window win;
            UserBaseViewModel vm;
            
            switch (user.Role.Name)
            {

                case "Доктор":
                {
                    win = ActivatorUtilities.CreateInstance<DoctorWindow>(_sp);
                    vm = ActivatorUtilities.CreateInstance<DoctorViewModel>(_sp, user);

                    break;
                }
                
                default:
                {
                    throw new ArgumentException("нет такой роли в приложении, обратитесь к администратору базы данных, который плохо читал инструкцию");
                }

            }

            win.DataContext = vm;
            win.Position = thiswin.Position;

            win.Show();
            thiswin.Close();
        }
        else
        {
            BlinkButton();
        }
        
    }

    private async void BlinkButton()
    {
        
        ButtonText = "Учётная запись не найдена";

        await Task.Delay(1000);
        
        ButtonText = "Войти в учётную запись";
        
    }

}