using System;

namespace Voenkomat_Kursach.Models;

public class Cabinet
{
    
    public int Number { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }


    public Cabinet()
    {
        Number = -1;
        Name = "";
        Description = "";
    }
    
    public Cabinet(int number, string name, string description)
    {
        Number = number;
        Name = name;
        Description = description;
    }
    
}