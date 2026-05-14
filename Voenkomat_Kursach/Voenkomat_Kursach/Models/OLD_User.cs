namespace Voenkomat_Kursach.Models;

public class OLD_User
{
    
    public int Id { get; set; }
    public OLD_Role OldRole { get; set; }
    public string Post { get; set; }
    public OLD_Cabinet OldCabinet { get; set; }


    public OLD_User()
    {
        Id = -1;
        OldRole = new();
        Post = "";
        OldCabinet = new();
    }
    
    public OLD_User(int id, OLD_Role oldRole, string post, OLD_Cabinet oldCabinet)
    {
        Id = id;
        OldRole = oldRole;
        Post = post;
        OldCabinet = oldCabinet;
    }
    
}