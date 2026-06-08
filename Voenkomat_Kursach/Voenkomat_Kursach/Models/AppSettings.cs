using System.Collections.Generic;

namespace Voenkomat_Kursach.Models;

public class AppSettings
{
    
    public string ConnectionString { get; set; }
    public Dictionary<string, string> Roles { get; set; }


    public AppSettings()
    {
        ConnectionString = "";
        Roles = new Dictionary<string, string>();
    }

    public AppSettings(string connectionString,  Dictionary<string, string> roles)
    {
        ConnectionString = connectionString;
        Roles = roles;
    }
    
}