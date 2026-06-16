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

    [ObservableProperty] private Cabinet _selectedCab;
    [ObservableProperty] private int _cabPage;
    private void UpdateCabs() => Cabs = new(_cr.GetPage(CabPage, 10));
    [RelayCommand]
    public void NextPageCab()
    {
        CabPage++;
        UpdateCabs();
    }
    [RelayCommand]
    public void PrevPageCab()
    {
        CabPage--;
        UpdateCabs();
    }
    [RelayCommand]
    public void FirstPageCab()
    {
        CabPage = 0;
        UpdateCabs();
    }
    [RelayCommand]
    public void LastPageCab()
    {
        CabPage = _cr.Count() / 10;
        UpdateJobs();
    }
    [RelayCommand] public void AddCab()
    {
        _cr.Add(new(0, "название", "описание"));
        UpdateCabs();
    }
    [RelayCommand] public void UpdateCab()
    {
        _cr.Update(SelectedCab);
        UpdateJobs();
    }
    [RelayCommand] public void DeleteCab()
    {
        _cr.Delete(SelectedCab);
        UpdateJobs();
    }
    
    
    [ObservableProperty] private Job _selectedJob;
    [ObservableProperty] private int _jobPage;
    private void UpdateJobs() => Jobs = new(_jr.GetPage(JobPage, 10));
    [RelayCommand]
    public void NextPageJob()
    {
        JobPage++;
        UpdateJobs();
    }
    [RelayCommand]
    public void PrevPageJob()
    {
        JobPage--;
        UpdateJobs();
    }
    [RelayCommand]
    public void FirstPageJob()
    {
        JobPage = 0;
        UpdateJobs();
    }
    [RelayCommand]
    public void LastPageJob()
    {
        JobPage = _jr.Count() / 10;
        UpdateJobs();
    }
    [RelayCommand] public void AddJob()
    {
        _jr.Add(new(0, "должность"));
        UpdateJobs();
    }
    [RelayCommand] public void UpdateJob()
    {
        _jr.Update(SelectedJob);
        UpdateJobs();
    }
    [RelayCommand] public void DeleteJob()
    {
        _jr.Delete(SelectedJob);
        UpdateJobs();
    }
    
    
    [ObservableProperty] private ChecklistItem _selectedCheck;
    [ObservableProperty] private int _checkPage;
    private void UpdateChecks() => Checks = new(_chr.GetPage(CheckPage, 10));
    [RelayCommand]
    public void NextPageCheck()
    {
        CheckPage++;
        UpdateCabs();
    }
    [RelayCommand]
    public void PrevPageCheck()
    {
        CheckPage--;
        UpdateChecks();
    }
    [RelayCommand]
    public void FirstPageCheck()
    {
        CheckPage = 0;
        UpdateChecks();
    }
    [RelayCommand]
    public void LastPageCheck()
    {
        CheckPage = _chr.Count() / 10;
        UpdateChecks();
    }
    [RelayCommand] public void AddCheck()
    {
        _chr.Add(new(0, new Job(), "название", "описание"));
        UpdateChecks();
    }
    [RelayCommand] public void UpdateCheck()
    {
        _chr.Update(SelectedCheck);
        UpdateChecks();
    }
    [RelayCommand] public void DeleteCheck()
    {
        _chr.Delete(SelectedCheck);
        UpdateChecks();
    }
    
    
    [ObservableProperty] private Employee _selectedEmp;
    [ObservableProperty] private int _empPage;
    private void UpdateEmps() => Emps = new(_er.GetPage(EmpPage, 10));
    [RelayCommand]
    public void NextPageEmp()
    {
        EmpPage++;
        UpdateEmps();
    }
    [RelayCommand]
    public void PrevPageEmp()
    {
        EmpPage--;
        UpdateEmps();
    }
    [RelayCommand]
    public void FirstPageEmp()
    {
        EmpPage = 0;
        UpdateEmps();
    }
    [RelayCommand]
    public void LastPageEmp()
    {
        EmpPage = _er.Count() / 10;
        UpdateEmps();
    }
    [RelayCommand] public void AddEmp()
    {
        _er.Add(new(0, "Иван Иванович Иванов", new(), new()));
        UpdateChecks();
    }
    [RelayCommand] public void UpdateEmp()
    {
        _er.Update(SelectedEmp);
        UpdateEmps();
    }
    [RelayCommand] public void DeleteEmp()
    {
        _er.Delete(SelectedEmp);
        UpdateEmps();
    }
    
    
    [ObservableProperty] private User _selectedUser;
    [ObservableProperty] private int _userPage;
    private void UpdateUsers() => Users = new(_ur.GetPage(EmpPage, 10));
    [RelayCommand]
    public void NextPageUser()
    {
        UserPage++;
        UpdateUsers();
    }
    [RelayCommand]
    public void PrevPageUser()
    {
        UserPage--;
        UpdateUsers();
    }
    [RelayCommand]
    public void FirstPageUser()
    {
        UserPage = 0;
        UpdateUsers();
    }
    [RelayCommand]
    public void LastPageUser()
    {
        UserPage = _ur.Count() / 10;
        UpdateUsers();
    }
    [RelayCommand] public void AddUser()
    {
        _ur.Add(new(0, new(), "", "", new()));
        UpdateChecks();
    }
    [RelayCommand] public void UpdateUser()
    {
        _ur.Update(SelectedUser);
        UpdateUsers();
    }
    [RelayCommand] public void DeleteUser()
    {
        _ur.Delete(SelectedUser);
        UpdateUsers();
    }
    
    
    [ObservableProperty] private Role _selectedRole;
    [ObservableProperty] private int _rolePage;
    private void UpdateRoles() => Roles = new(_rr.GetPage(EmpPage, 10));
    [RelayCommand]
    public void NextPageRole()
    {
        RolePage++;
        UpdateRoles();
    }
    [RelayCommand]
    public void PrevPageRole()
    {
        RolePage--;
        UpdateRoles();
    }
    [RelayCommand]
    public void FirstPageRole()
    {
        RolePage = 0;
        UpdateRoles();
    }
    [RelayCommand]
    public void LastPageRole()
    {
        RolePage = _rr.Count() / 10;
        UpdateRoles();
    }
    [RelayCommand] public void AddRole()
    {
        _rr.Add(new(0, "", false));
        UpdateRoles();
    }
    [RelayCommand] public void UpdateRole()
    {
        _rr.Update(SelectedRole);
        UpdateRoles();
    }
    [RelayCommand] public void DeleteRole()
    {
        _rr.Delete(SelectedRole);
        UpdateRoles();
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