namespace Voenkomat_Kursach.Models;

public class Role
{
    
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsMed { get; set; }


    public Role()
    {
        Id = -1;
        Name = "";
        IsMed = false;
    }
    
    public Role(int id, string name, bool isMed)
    {
        Id = id;
        Name = name;
        IsMed = isMed;
    }
    
}