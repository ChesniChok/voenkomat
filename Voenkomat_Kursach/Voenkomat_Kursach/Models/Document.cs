using System;

namespace Voenkomat_Kursach.Models;

public class Document
{
    
    public int Id { get; set; }
    public string Type { get; set; }
    public string Number { get; set; }
    public string FileName { get; set; }
    public Employee Author { get; set; }
    public Dude Dude { get; set; }
    public MedComission MedComisiion { get; set; }
    public string Description { get; set; }


    public Document()
    {
        Id = -1;
        Type = "";
        Number = "";
        FileName = "";
        Author = new();
        Dude = new();
        MedComisiion = new();
        Description = "";
    }
    
    public Document(int id, string type, string number, string fileName, Employee author, Dude dude, MedComission medComisiion, string description)
    {
        Id = id;
        Type = type;
        Number = number;
        FileName = fileName;
        Author = author;
        Dude = dude;
        MedComisiion = medComisiion;
        Description = description;
    }

}