using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Voenkomat_Kursach.Models;
using Voenkomat_Kursach.Views;

namespace Voenkomat_Kursach.ViewModels;

public partial class AdminViewModel : UserBaseViewModel
{

    private String c;
    private Dictionary<string, string> _roles;

    public AdminViewModel(IServiceProvider sp, User user, Window win, Window backWin, IOptions<AppSettings> ap) : base(sp, user, win, backWin)
    {
        c = ap.Value.ConnectionString;
        _roles = ap.Value.Roles;
    }


    [ObservableProperty] private string _s;
    

    [ObservableProperty] private string _server;
    [ObservableProperty] private string _serverVal;
    
    [ObservableProperty] private string _user;
    [ObservableProperty] private string _userVal;
    
    [ObservableProperty] private string _password;
    [ObservableProperty] private string _passwordVal;
    
    [ObservableProperty] private string _database;
    [ObservableProperty] private string _databaseVal;
    
    [RelayCommand]
    public void Test()
    {

        var r = new Random();

        S = _roles[_roles.Keys.ElementAt(r.Next(_roles.Count))];
        
        
        {
            
            var s = c.Split(";");

            {
                
                var ss = s[0].Split("=");

                Server = ss[0];
                ServerVal = ss[1];
                
            }
            
            {
                
                var ss = s[1].Split("=");

                User = ss[0];
                UserVal = ss[1];
                
            }
            
            {
                
                var ss = s[2].Split("=");

                Password = ss[0];
                PasswordVal = ss[1];
                
            }
            
            {
                
                var ss = s[3].Split("=");

                Database = ss[0];
                DatabaseVal = ss[1];
                
            }
            
        }

    }
    
    
}