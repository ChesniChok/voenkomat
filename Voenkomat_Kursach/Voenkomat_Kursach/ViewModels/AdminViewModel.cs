using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Options;
using Voenkomat_Kursach.DB;
using Voenkomat_Kursach.Models;

namespace Voenkomat_Kursach.ViewModels;

public partial class AdminViewModel : UserBaseViewModel
{
    
    private AppSettings _ap;

    private CabinetRepository _cr;
    private JobRepository _jr;
    private ChecklistItemRepository _chr;
    private EmployeeRepository _er;
    private UserRepository _ur;
    private RoleRepository _rr;

    public AdminViewModel(IServiceProvider sp, 
        CabinetRepository cr, JobRepository jr, ChecklistItemRepository chr, EmployeeRepository er, UserRepository ur, RoleRepository rr,
        User user, Window win, AppSettings ap) : base(sp, user, win)
    {
        
        _ap = ap;
        
        _cr = cr;
        _jr = jr;
        _chr = chr;
        _er = er;
        _ur = ur;
        _rr = rr;
        
        GetConnectionSettings();
        GetDictionarySettings();
        GetCollections();
        
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
    

    [ObservableProperty] private ObservableCollection<Cabinet> _cabs;
    [ObservableProperty] private ObservableCollection<Job> _jobs;
    [ObservableProperty] private ObservableCollection<ChecklistItem> _checks;
    [ObservableProperty] private ObservableCollection<Employee> _emps;
    [ObservableProperty] private ObservableCollection<User> _users;
    [ObservableProperty] private ObservableCollection<Role> _roles;





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

    private void GetCollections()
    {

        Cabs = new(_cr.GetPage(0, 10));
        Jobs = new(_jr.GetPage(0, 10));
        Checks = new(_chr.GetPage(0, 10));
        Emps = new(_er.GetPage(0, 10));
        Users = new(_ur.GetPage(0, 10));
        Roles = new(_rr.GetPage(0, 10));

    }

    [ObservableProperty] private Job _selectedJob;



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
            Error(e.Message);
        }
    }

    [RelayCommand]
    public void ImportSettings()//загрузка настроек из файла по указанному пути в файл настроек
    {

        AppSettings sets;
        
        try
        {
            using (var fs = File.OpenRead(FilePath))
            {
                
                sets = JsonSerializer.Deserialize<AppSettings>(fs);

                _ap = sets;

            }
        }
        catch (Exception e)
        {
            Error(e.Message);
            return;
        }
        
        
        
        GetConnectionSettings();
        GetDictionarySettings();
        
        SerializeSettings();
        
    }


    private void SerializeSettings(string path = "appsettings.json")//сохранить настройки в файл
    {

        var sets = new AppSettings(GenerateConnectionString(), GenerateDictionary());

        using(var fs = File.Create(path))
        {
            JsonSerializer.Serialize(fs, sets);
        }

    }

    private string GenerateConnectionString()
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

    private void Error(string message)
    {
        SystemMessage = message;
        Console.WriteLine('\a');
    }

    protected override void GoBack() => GoToMain(_win);
    
}