namespace Voenkomat_Kursach.Models;

public class User
{
    
    public int Id { get; set; }
    public Employee Employee { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }


    public User()
    {
        Id = -1;
        Employee = new();
        Login = "логин";
        Password = "пароль";
        Role = new();
    }
    
    public User(int id, Employee employee, string login, string password, Role role)
    {
        Id = id;
        Employee = employee;
        Login = login;
        Password = password;
        Role = role;
    }
    
}