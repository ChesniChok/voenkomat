namespace Voenkomat_Kursach.Models;

public class OLD_Cabinet
{
    
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }


    public OLD_Cabinet()
    {
        Id = -1;
        Name = "";
        Description = "";
    }
    
    public OLD_Cabinet(int id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }
    
}