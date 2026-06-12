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

    private String _connectionString;
    private Dictionary<string, string> _roles;

    public AdminViewModel(IServiceProvider sp, User user, Window win, IOptions<AppSettings> ap) : base(sp, user, win)
    {
        
        _connectionString = ap.Value.ConnectionString;
        _roles = ap.Value.Roles;
        
        GetConnectionSettings();
        GetDictionarySettings();
        
    }


    [ObservableProperty] private string _systemMessage;

    [ObservableProperty] private string _serverVal;
    [ObservableProperty] private string _userVal;
    [ObservableProperty] private string _passwordVal;
    [ObservableProperty] private string _databaseVal;
    
    [ObservableProperty] private string _filePath;
    
    [ObservableProperty] private string _adm;
    [ObservableProperty] private string _arc;
    [ObservableProperty] private string _reg;
    [ObservableProperty] private string _doc;
    [ObservableProperty] private string _com;
    
    

    

    private void GetConnectionSettings()
    {
        
        var s = _connectionString.Split(";");

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

    private void GetDictionarySettings()
    {
        

            Adm = _roles.First(r => r.Value == "admin").Key;

            Arc = _roles.First(r => r.Value == "arch").Key;

            Reg = _roles.First(r => r.Value == "reg").Key;

            Doc = _roles.First(r => r.Value == "doctor").Key;

            Com = _roles.First(r => r.Value == "comis").Key;
 
        
    }



    [RelayCommand]
    public void SaveSettings() => SerializeSettings();//сохранить введённые знаечения в файл настроек
    
    [RelayCommand]
    public void ExportSettings()//сохранить файл настроек в указанный путь
    {
        
        try
        {
            SerializeSettings(FilePath);
        }
        catch (Exception e)
        {
            SystemMessage = e.Message;
            Pilin();
        }
        
    }

    [RelayCommand]
    public void ImportSettings()//загрузка настроек из файла по указанному пути в файл настроек
    {

        AppSettings sets = new AppSettings();
        
        try
        {
            using (var fs = File.OpenRead(FilePath))
            {
                
                sets = JsonSerializer.Deserialize<AppSettings>(fs);
                
                _connectionString = sets.ConnectionString;
                _roles = sets.Roles;
                
            }
        }
        catch (Exception e)
        {
            SystemMessage = e.Message;
            Pilin();
            return;
        }
        
        
        
        GetConnectionSettings();
        
        SerializeSettings();
        
    }


    private void SerializeSettings(string path = "appsettings.json")//сохранить настройки в файл
    {

        var sets = new AppSettings(GenerateConectionString(), _roles);

        using(var fs = File.Create(path))
        {
            JsonSerializer.Serialize(fs, sets);
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

    private void Pilin()//издать системгный звук
    {
        Console.WriteLine('\a');
    }

    protected override void GoBack() => GoToMain(_win);
    
}