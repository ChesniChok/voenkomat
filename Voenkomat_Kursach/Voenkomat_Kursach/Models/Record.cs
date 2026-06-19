using System;

namespace Voenkomat_Kursach.Models;

public class Record
{
    
    public int Id { get; set; }
    public string Type { get; set; }
    public Employee? Author { get; set; }
    public MedComission? MedComisiion { get; set; }
    public string Content {get; set;}
    public string Description { get; set; }


    public Record()
    {
        Id = 0;
        Type = "";
        Author = new();
        MedComisiion = new();
        Content = "";
        Description = "";
    }
    
    public Record(int id, string type, Employee? author, MedComission? medComisiion, string content, string description)
    {
        Id = id;
        Type = type;
        Author = author;
        MedComisiion = medComisiion;
        Content = content;
        Description = description;
    }

}