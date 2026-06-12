using System.Collections.Generic;

namespace Voenkomat_Kursach.Models;

public class AppSettings
{
    
    public string ConnectionString { get; set; }
    public Dictionary<string, string> Roles { get; set; }


    public AppSettings()
    {
        
        ConnectionString = "server=hypixel;user=reus;password=qwerty;database=baza";
        Roles = new Dictionary<string, string>();
        
        Roles.Add("админ", "admin");
        Roles.Add("архивариус", "arch");
        Roles.Add("регистрирующий", "reg");
        Roles.Add("Врач", "doctor");
        Roles.Add("комиссионщик", "comis");
        
    }

    public AppSettings(string connectionString,  Dictionary<string, string> roles)
    {
        ConnectionString = connectionString;
        Roles = roles;
    }
    
}