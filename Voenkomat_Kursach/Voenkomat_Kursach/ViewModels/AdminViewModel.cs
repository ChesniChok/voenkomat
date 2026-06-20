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

    private JobRepository _jr;
    private ChecklistItemRepository _chr;
    private EmployeeRepository _er;
    private UserRepository _ur;
    private RoleRepository _rr;

    public AdminViewModel(IServiceProvider sp, JobRepository jr, ChecklistItemRepository chr, EmployeeRepository er, UserRepository ur, RoleRepository rr, 
        User user, Window win, AppSettings ap) : base(sp, user, win)
    {
        
        _ap = ap;
        
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
    [ObservableProperty] private string _reg;
    [ObservableProperty] private string _doc;
    [ObservableProperty] private string _com;
    

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

        Reg = _ap.Roles.First(r => r.Value == "reg").Key;

        Doc = _ap.Roles.First(r => r.Value == "doctor").Key;

        Com = _ap.Roles.First(r => r.Value == "comis").Key;
        
    }

    private void GetCollections()
    {

        Jobs = new(_jr.GetPage(0, 10));
        Checks = new(_chr.GetPage(0, 10));
        Emps = new(_er.GetPage(0, 10));
        Users = new(_ur.GetPage(0, 10));
        Roles = new(_rr.GetPage(0, 10));

    }
    
    
    
    [ObservableProperty] private Job _selectedJob;
    [ObservableProperty] private int _jobPage;
    [RelayCommand] private void UpdateJobs() => Jobs = new(_jr.GetPage(JobPage, 10));
    [RelayCommand]
    public void NextPageJob()
    {
        if (JobPage >= _jr.Count() / 10) return;
        JobPage += 10;
        UpdateJobs();
    }
    [RelayCommand]
    public void PrevPageJob()
    {
        if (JobPage == 0) return;
        JobPage -= 10;
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
        JobPage = (_jr.Count()-1) / 10 * 10;
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
    [ObservableProperty] private bool _checkJobNull;
    [RelayCommand] private void UpdateChecks() => Checks = new(_chr.GetPage(CheckPage, 10));
    [RelayCommand]
    public void NextPageCheck()
    {
        if (CheckPage == _chr.Count() / 10) return;
        CheckPage += 10;
        UpdateChecks();
    }
    [RelayCommand]
    public void PrevPageCheck()
    {
        if (CheckPage == 0) return;
        CheckPage -= 10;
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
        CheckPage = (_chr.Count()-1) / 10 * 10;
        UpdateChecks();
    }
    [RelayCommand] public void AddCheck()
    {
        _chr.AddNull(new(0, new Job(null, ""), "", ""));
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
    [RelayCommand] private void UpdateEmps() => Emps = new(_er.GetPage(EmpPage, 10));
    [RelayCommand]
    public void NextPageEmp()
    {
        if (EmpPage == _er.Count() / 10) return;
        EmpPage += 10;
        UpdateEmps();
    }
    [RelayCommand]
    public void PrevPageEmp()
    {
        if (EmpPage == 0) return;
        EmpPage -= 10;
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
        EmpPage = (_er.Count()-1) / 10 * 10;
        UpdateEmps();
    }
    [RelayCommand] public void AddEmp()
    {
        _er.Add(new(0, "Иван Иванович Иванов", new(1, "")));
        UpdateEmps();
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
    [RelayCommand] private void UpdateUsers() => Users = new(_ur.GetPage(EmpPage, 10));
    [RelayCommand]
    public void NextPageUser()
    {
        if (UserPage == _ur.Count()) return;
        UserPage += 10;
        UpdateUsers();
    }
    [RelayCommand]
    public void PrevPageUser()
    {
        if (EmpPage == 0) return;
        UserPage -= 10;
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
        UserPage = (_ur.Count()-1) / 10 * 10;
        UpdateUsers();
    }
    [RelayCommand] public void AddUser()
    {
        _ur.Add(new(0, new(1, "", new(1, "")), "", "", new(1, "", false)));
        UpdateUsers();
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
    [RelayCommand] private void UpdateRoles() => Roles = new(_rr.GetPage(EmpPage, 10));
    [RelayCommand]
    public void NextPageRole()
    {
        if (RolePage == _rr.Count() / 10) return;
        RolePage += 10;
        UpdateRoles();
    }
    [RelayCommand]
    public void PrevPageRole()
    {
        if (RolePage == 0) return;
        RolePage -= 10;
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
        RolePage = (_rr.Count()-1) / 10 * 10;
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