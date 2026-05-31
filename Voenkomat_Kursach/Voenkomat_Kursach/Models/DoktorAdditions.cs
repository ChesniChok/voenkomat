namespace Voenkomat_Kursach.Models;

public class DoktorAdditions
{
    
    public int Id { get; set; }
    public string Value { get; set; }


    public DoktorAdditions()
    {
        Id = -1;
        Value = "";
    }
    
    public DoktorAdditions(int id, string value)
    {
        Id = id;
        Value = value;
    }
    
}