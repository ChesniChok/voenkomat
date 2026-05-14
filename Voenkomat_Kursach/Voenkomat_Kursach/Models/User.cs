namespace Voenkomat.Models;

public class User
{
    public int Id { get; set; }
    public Role Role { get; set; }
    public string Post { get; set; }
    public Cabinet Cabinet { get; set; }
    
    public User()
    {
        Id = -1;
        Role = new();
        Post = "";
        Cabinet = new();
    }
    
    public User(int id, Role role, string post, Cabinet cabinet)
    {
        Id = id;
        Role = role;
        Post = post;
        Cabinet = cabinet;
    }
}