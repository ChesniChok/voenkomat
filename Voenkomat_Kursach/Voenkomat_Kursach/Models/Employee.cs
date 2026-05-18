namespace Voenkomat_Kursach.Models;

public class Employee
{
    
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Password { get; set; }
    public Job Job { get; set; }
    public Role Role { get; set; }
    public Cabinet Cabinet { get; set; }


    public Employee()
    {
        Id = -1;
        FullName = "";
        Password = "";
        Job = new();
        Role = new();
        Cabinet = new();
    }
    
    public Employee(int id, string fullName, string password, Job job, Role role, Cabinet cabinet)
    {
        Id = id;
        FullName = fullName;
        Password = password;
        Job = job;
        Role = role;
        Cabinet = cabinet;
    }
    
}