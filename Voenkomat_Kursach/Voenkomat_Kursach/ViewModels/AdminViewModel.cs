using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Options;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.ViewModels;

public partial class AdminViewModel : UserBaseViewModel
{

    private String c;
    private Dictionary<string, string> _roles;

    public AdminViewModel(IServiceProvider sp, User user, Window win, IOptions<AppSettings> ap) : base(sp, user, win)
    {
        c = ap.Value.ConnectionString;
        _roles = ap.Value.Roles;
    }


    [ObservableProperty] private string _s;
    

    [ObservableProperty] private string _serverVal;
    [ObservableProperty] private string _userVal;
    [ObservableProperty] private string _passwordVal;
    [ObservableProperty] private string _databaseVal;
    
    
    
    [RelayCommand]
    public void Test()
    {

        var r = new Random();

        S = _roles[_roles.Keys.ElementAt(r.Next(_roles.Count))];
        
        ConnectionSettings();

    }

    private void ConnectionSettings()
    {
        
        var s = c.Split(";");

        {
            var ss = s[0].Split("=");
            
            ServerVal = ss[1];
        }
            
        {
            var ss = s[1].Split("=");

            UserVal = ss[1];
        }
            
        {
            var ss = s[2].Split("=");

            PasswordVal = ss[1];
        }
            
        {
            var ss = s[3].Split("=");

            DatabaseVal = ss[1];
        }
        
    }



    [RelayCommand]
    public void SaveSettings()
    {
        
        SerializeSettings();
        
    }

    private void SerializeSettings()
    {

        var sets = new AppSettings(GenerateConectionString(), _roles);

        using(var fs = File.Create("appsettings.json"))
        {
            JsonSerializer.Serialize(fs, sets);

            ServerVal = fs.Name;
        }

    }

    private string GenerateConectionString()
    {

        var sb = new StringBuilder();

        sb.Append("server=");
        sb.Append(ServerVal);
        
        sb.Append(";user=");
        sb.Append(UserVal);
        
        sb.Append(";password=");
        sb.Append(PasswordVal);
        
        sb.Append(";database=");
        sb.Append(DatabaseVal);
        
        return sb.ToString();

    }

    protected override void GoBack() => GoToMain(_win);
    
}