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
    
    private AppSettings _ap;

    public AdminViewModel(IServiceProvider sp, User user, Window win, AppSettings ap) : base(sp, user, win)
    {
        
        _ap = ap;
        
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
        
        var s = _ap.ConnectionString.Split(";");

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
        
        Adm = _ap.Roles.First(r => r.Value == "admin").Key;

        Arc = _ap.Roles.First(r => r.Value == "arch").Key;

        Reg = _ap.Roles.First(r => r.Value == "reg").Key;

        Doc = _ap.Roles.First(r => r.Value == "doctor").Key;

        Com = _ap.Roles.First(r => r.Value == "comis").Key;
        
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
                
                _ap.ConnectionString = sets.ConnectionString;
                _ap.Roles = sets.Roles;
                
            }
        }
        catch (Exception e)
        {
            SystemMessage = e.Message;
            Pilin();
            return;
        }
        
        
        
        GetConnectionSettings();
        GetDictionarySettings();
        
        SerializeSettings();
        
    }


    private void SerializeSettings(string path = "appsettings.json")//сохранить настройки в файл
    {

        var sets = new AppSettings(GenerateConectionString(), GenerateDictionary());

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

    private Dictionary<string, string> GenerateDictionary()
    {
        
        Dictionary<string, string> dict = new();
        
        dict.Add(Adm, "admin");
        dict.Add(Arc, "arch");
        dict.Add(Reg, "reg");
        dict.Add(Doc, "doctor");
        dict.Add(Com, "comis");
        
        return dict;
        
    }

    private void Pilin()//издать системгный звук
    {
        Console.WriteLine('\a');
    }

    protected override void GoBack() => GoToMain(_win);
    
}