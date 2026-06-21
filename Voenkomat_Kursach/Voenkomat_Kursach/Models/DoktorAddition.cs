namespace Voenkomat_Kursach.Models;

public class DoktorAddition
{
    
    public int Id { get; set; }
    public string Value { get; set; }


    public DoktorAddition()
    {
        Id = 0;
        Value = "";
    }
    
    public DoktorAddition(int id, string value)
    {
        Id = id;
        Value = value;
    }
    
}