namespace Voenkomat_Kursach.Models;

public class Job
{
    
    public int? Id { get; set; }
    public string Name { get; set; }


    public Job()
    {
        Id = 0;
        Name = "название";
    }
    
    public Job(int? id, string name)
    {
        Id = id;
        Name = name;
    }
    
}