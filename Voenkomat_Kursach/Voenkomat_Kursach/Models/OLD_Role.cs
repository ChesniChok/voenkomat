namespace Voenkomat_Kursach.Models;

public class OLD_Role
{
    
    public int Id { get; set; }
    public string Name { get; set; }


    public OLD_Role()
    {
        Id = -1;
        Name = "";
    }
    
    public OLD_Role(int id, string name)
    {
        Id = id;
        Name = name;
    }
    
}