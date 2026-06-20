namespace Voenkomat_Kursach.Models;

public class Employee
{
    
    public int Id { get; set; }
    public string FullName { get; set; }
    public Job Job { get; set; }


    public Employee()
    {
        Id = 0;
        FullName = "имя";
        Job = new();
    }
    
    public Employee(int id, string fullName, Job job)
    {
        Id = id;
        FullName = fullName;
        Job = job;
    }
    
}