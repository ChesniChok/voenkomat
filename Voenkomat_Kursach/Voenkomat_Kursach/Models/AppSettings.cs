using System.Collections.Generic;

namespace Voenkomat_Kursach.Models;

public class AppSettings
{
    
    public string ConnectionString { get; set; }
    
    public Dictionary<string, string> Roles { get; set; }
    
}