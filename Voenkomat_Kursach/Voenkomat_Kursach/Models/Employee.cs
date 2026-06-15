namespace Voenkomat_Kursach.Models;

public class Employee
{
    
    public int Id { get; set; }
    public string FullName { get; set; }
    public Cabinet Cabinet { get; set; }
    public Job Job { get; set; }


    public Employee()
    {
        Id = -1;
        FullName = "имя";
        Cabinet = new();
        Job = new();
    }
    
    public Employee(int id, string fullName, Cabinet cabinet, Job job)
    {
        Id = id;
        FullName = fullName;
        Cabinet = cabinet;
        Job = job;
    }
    
}