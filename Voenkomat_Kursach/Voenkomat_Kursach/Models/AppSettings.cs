using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Voenkomat_Kursach.Models;

public class AppSettings
{
    
    public string ConnectionString { get; set; }
    public Dictionary<string, string> Roles { get; set; }

    
    public AppSettings()
    {
        
        ConnectionString = "server=revres;user=reus;password=drowssap;database=esabatad";
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


    public void Load(string path)
    {
        using (var fs = File.OpenRead(path))
        {
            var sets = JsonSerializer.Deserialize<AppSettings>(fs);
            
            
            ConnectionString = sets.ConnectionString;
            Roles = sets.Roles;
        }
    }
    
}